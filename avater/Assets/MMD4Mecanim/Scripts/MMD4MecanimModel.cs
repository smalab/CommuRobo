﻿using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using MorphCategory				= MMD4MecanimData.MorphCategory;
using MorphType					= MMD4MecanimData.MorphType;
using MorphAutoLumninousType	= MMD4MecanimData.MorphAutoLumninousType;
using MorphData					= MMD4MecanimData.MorphData;
using MorphMotionData			= MMD4MecanimData.MorphMotionData;
using BoneData					= MMD4MecanimData.BoneData;
using RigidBodyData				= MMD4MecanimData.RigidBodyData;

using Bone						= MMD4MecanimBone;

using IMorph					= MMD4MecanimAnim.IMorph;
using IAnimModel				= MMD4MecanimAnim.IAnimModel;

[ExecuteInEditMode ()] // for Morph
public partial class MMD4MecanimModel : MonoBehaviour, IAnimModel
{
	public const int MaterialEdgeRenderQueue = 2001; // NEXTEdge
	public const int MaterialBaseRenderQueue = 2002; // Greater than NEXTEdge

	// Memo: Skybox = 2500
	public const int MaterialEdgeRenderQueue_AfterSkybox = 2501; // NEXTEdge
	public const int MaterialBaseRenderQueue_AfterSkybox = 2502; // Greater than Skybox & NEXTEdge

	public enum PhysicsEngine
	{
		None,
		BulletPhysics,
	}

	public class Morph : IMorph
	{
		public float							weight;				// Set by manually.(Not animation.)
		public float							weight2;			// Set by MMD4MecanimMorphHelper(Manually overwrite weight. Limit default morph every categories(EyeBlow / Eye / Lip).)

		public float							_animWeight;		// Set by _UpdateAnimModel()
		public float							_appendWeight;		// Set by group morph.
		public float							_updateWeight;
		public float							_updatedWeight;

		public MorphData						morphData;
		public MorphAutoLumninousType			morphAutoLuminousType;

		public MorphType morphType {
			get {
				if( morphData != null ) {
					return morphData.morphType;
				}
				return MorphType.Group;
			}
		}

		public MorphCategory morphCategory {
			get {
				if( morphData != null ) {
					return morphData.morphCategory;
				}
				return MorphCategory.Base;
			}
		}

		public string name {
			get {
				if( morphData != null ) {
					return morphData.nameJp;
				}
				return null;
			}
		}

		public string translatedName {
			get {
				if( morphData != null ) {
					return morphData.translatedName;
				}

				return null;
			}
		}

		string IMorph.name {
			get { return this.name; }
		}

		float IMorph.weight {
			get { return this.weight; }
			set { this.weight = value; }
		}
	}
	
	public class MorphAutoLuminous
	{
		public float lightUp;
		public float lightOff;
		public float lightBlink;
		public float lightBS;

		public bool updated;
	}

	[System.Serializable]
	public class RigidBody
	{
		[System.NonSerialized]
		public RigidBodyData					rigidBodyData = null;
		public bool								freezed = true;
		public int								_freezedCached = -1;
	}

	[System.Serializable]
	public class Anim : MMD4MecanimAnim.Anim
	{
	}

	[System.Serializable]
	public class BulletPhysics
	{
		public bool joinLocalWorld = true;
		public bool useOriginalScale = true;
		public bool useCustomResetTime = false;
		public float resetMorphTime = 1.8f;
		public float resetWaitTime = 1.2f;
		public MMD4MecanimInternal.Bullet.WorldProperty worldProperty;
		public MMD4MecanimInternal.Bullet.MMDModelProperty mmdModelProperty;
	}
	
	public class CloneMesh
	{
		public SkinnedMeshRenderer					skinnedMeshRenderer;
		public Mesh									mesh;
		public Vector3[]							vertices;
		public Vector3[]							normals;				// for XDEF
		public Matrix4x4[]							bindposes;				// for XDEF
		public BoneWeight[]							boneWeights;			// for XDEF
	}

	public class CloneMaterial
	{
		public Material[]							materials;
		public MMD4MecanimData.MorphMaterialData[]	materialData;
		public MMD4MecanimData.MorphMaterialData[]	backupMaterialData;
		public bool[]								updateMaterialData;
	}

	private struct MorphBlendShape
	{
		// Key ... morphID Value ... blendShapeIndex
		public int[]								blendShapeIndices;
	}

	public float 									importScale = 0.01f;
	public bool										initializeOnAwake = false;
	public bool										postfixRenderQueue = true;
	public bool										renderQueueAfterSkybox = false;
	public bool										updateWhenOffscreen = true;
	public bool										animEnabled = true;
	public bool										animSyncToAudio = true;
	public TextAsset								modelFile;
	public TextAsset								indexFile;
	public TextAsset								vertexFile;
	public AudioSource								audioSource;

	public int										animDelayedAwakeFrame = 1;

	public bool boneInherenceEnabled				= false;
	public bool boneMorphEnabled					= false;

	public bool pphEnabled							= true;
	public bool pphEnabledNoAnimation				= true;

	public bool										pphShoulderEnabled = true;
	public float									pphShoulderFixRate = 0.7f;

	public bool										ikEnabled = false;
	public bool										fullBodyIKEnabled = false;
	public bool										vertexMorphEnabled = true;
	public bool										materialMorphEnabled = true;
	public bool										blendShapesEnabled = true;
	public bool										xdefEnabled = false;
	public bool										xdefNormalEnabled = false;
	public bool										xdefMobileEnabled = false;
	bool											_blendShapesEnabledCache;
	float											_pphShoulderFixRateImmediately = 0.0f;

	public float									generatedColliderMargin = 0.01f;

	public float pphShoulderFixRateImmediately { get {return _pphShoulderFixRateImmediately; } }

	public enum PPHType
	{
		Shoulder,
	}

	public MMD4MecanimData.ModelData modelData {
		get { return _modelData; }
	}
	
	public byte[] modelFileBytes {
		get { return (modelFile != null) ? modelFile.bytes : null; }
	}

	public byte[] indexFileBytes {
		get { return (indexFile != null) ? indexFile.bytes : null; }
	}

	public byte[] vertexFileBytes {
		get { return (vertexFile != null) ? vertexFile.bytes : null; }
	}

	[NonSerialized]
	public Bone[]									boneList;
	[NonSerialized]
	public IK[]										ikList;
	[NonSerialized]
	public Morph[]									morphList;
	[NonSerialized]
	public MorphAutoLuminous						morphAutoLuminous;

	public PhysicsEngine							physicsEngine;
	PhysicsEngine									_physicsEngineCached;
	public BulletPhysics							bulletPhysics;
	public RigidBody[]								rigidBodyList;

	public Anim[]									animList;

	private bool									_initialized;
	private Bone									_rootBone;
	private Bone[]									_sortedBoneList;
	private List<Bone>								_processingBoneList;
	private MeshRenderer[]							_meshRenderers;
	private SkinnedMeshRenderer[]					_skinnedMeshRenderers;
	private CloneMesh[]								_cloneMeshes;
	private MorphBlendShape[]						_morphBlendShapes;
	private CloneMaterial[]							_cloneMaterials;
	#if MMD4MECANIM_DEBUG
	private bool									_supportDebug;
	#endif
	private bool									_supportDeferred;
	private Light									_deferredLight;

	public MMD4MecanimData.ModelData				_modelData;

	public bool skinningEnabled {
		get {
			return _skinnedMeshRenderers != null && _skinnedMeshRenderers.Length > 0;
		}
	}

	// for Inspector.
	public enum EditorViewPage {
		Model,
		Bone,
		IK,
		Morph,
		Anim,
		Physics,
	}

	public enum EditorViewMorphNameType {
		Japanese,
		English,
		Translated,
	}

	[HideInInspector]
	public EditorViewPage							editorViewPage;
	[HideInInspector]
	public byte										editorViewMorphBits = 0x0f;
	[HideInInspector]
	public bool										editorViewRigidBodies = false;
	[HideInInspector]
	public EditorViewMorphNameType					editorViewMorphNameType = EditorViewMorphNameType.Japanese;
	[NonSerialized]
	public Mesh										defaultMesh;
	
	MMD4MecanimBulletPhysics.MMDModel				_bulletPhysicsMMDModel;

	// Specially actions.
	public System.Action							onUpdating;
	public System.Action							onUpdated;
	public System.Action							onLateUpdating;
	public System.Action							onLateUpdated;

	public Bone GetRootBone()
	{
		return _rootBone;
	}

	public Bone GetBone( int boneID )
	{
		if( this.boneList != null ) {
			if( boneID >= 0 && boneID < this.boneList.Length ) {
				return this.boneList[boneID];
			}
		}

		return null;
	}

	void Awake()
	{
		if( initializeOnAwake ) {
			Initialize();
		}
	}
	
	void Start()
	{
		Initialize();
	}

	void Update()
	{
		if( !Application.isPlaying ) {
			return;
		}

		_Salvage();

		if( this.onUpdating != null ) {
			this.onUpdating();
		}

		_UpdateAnim();
		_UpdateMorph();

		_UpdateBone();
		_UpdateIK();

		if( this.onUpdated != null ) {
			this.onUpdated();
		}
	}

	void _Salvage()
	{
		if( _meshRenderers == null || _skinnedMeshRenderers == null ) {
			#if MMD4MECANIM_DEBUG
			Debug.LogWarning( "Recreate: _InitializeMesh()" );
			#endif
			_InitializeMesh();
		}
		
		if( this.modelFile != null ) {
			if( _modelData == null ) {
				#if MMD4MECANIM_DEBUG
				Debug.LogWarning( "Recreate: _InitializeModelData()" );
				#endif
				_InitializeModelData();
			}

			if( _modelData != null ) {
				int boneLength = (this.boneList != null) ? this.boneList.Length : 0;
				int boneDataLength = (_modelData.boneDataList != null) ? _modelData.boneDataList.Length : 0;
				if( boneLength != boneDataLength ) {
					#if MMD4MECANIM_DEBUG
					Debug.LogWarning( "Recreate: _InitializeModel()" );
					#endif
					_InitializeModel();
				}
				
				int rigidBodyLength = (this.rigidBodyList != null) ? this.rigidBodyList.Length : 0;
				int rigidBodyDataLength = (_modelData.rigidBodyDataList != null) ? _modelData.rigidBodyDataList.Length : 0;
				if( rigidBodyLength != rigidBodyDataLength ) {
					#if MMD4MECANIM_DEBUG
					Debug.LogWarning( "Recreate: _InitializeRigidBody()" );
					#endif
					_InitializeRigidBody();
				}

				if( _morphBlendShapes == null ) {
					if( _modelData.morphDataList != null && _modelData.morphDataList.Length > 0 ) {
						if( this.blendShapesEnabled && _IsBlendShapesAnything() ) {
							_blendShapesEnabledCache = this.blendShapesEnabled;
							#if MMD4MECANIM_DEBUG
							Debug.LogWarning( "Recreate: _InitializeBlendShapes()" );
							#endif
							_PrepareBlendShapes();
							_InitializeBlendShapes();
						}
					}
				}

				if( _animator == null || _animMorphCategoryWeights == null ) {
					#if MMD4MECANIM_DEBUG
					Debug.LogWarning( "Recreate: _InitializeAnimatoion()" );
					#endif
					_InitializeAnimatoion();
				}

				if( _bulletPhysicsMMDModel == null ) {
					if( this.physicsEngine == PhysicsEngine.None ||
						this.physicsEngine == PhysicsEngine.BulletPhysics ) {
						#if MMD4MECANIM_DEBUG
						Debug.LogWarning( "Recreate: _InitializePhysicsEngine()" );
						#endif
						_InitializePhysicsEngine();
					}
				}

				if( _animator != null &&
					_animator.avatar != null &&
					_animator.avatar.isValid &&
					_animator.avatar.isHuman ) {
					if( _leftArmBone == null || _rightArmBone == null ) {
						#if MMD4MECANIM_DEBUG
						Debug.LogWarning( "Recreate: _InitializePPHBones()" );
						#endif
						_InitializePPHBones();
					}
				}

				if( this.nextEdgeMaterial_Pass4 == null || this.nextEdgeMaterial_Pass4.shader == null ||
					this.nextEdgeMaterial_Pass8 == null || this.nextEdgeMaterial_Pass8.shader == null ) {
					#if MMD4MECANIM_DEBUG
					Debug.LogWarning( "Recreate: _InitializeNEXTMaterial()" );
					#endif
					_InitializeNEXTMaterial();
				}
			}
		}
	}

	void LateUpdate()
	{
		_UpdatedDeffered();
		_UpdatedNEXTEdge();
		if( !Application.isPlaying ) {
			_UpdateAmbientPreview();
		}

		if( !Application.isPlaying ) {
			return;
		}

		if( this.onLateUpdating != null ) {
			this.onLateUpdating();
		}

		_LateUpdateBone();
		_LateUpdateIK();
		_UploadMeshMaterial();

		if( this.onLateUpdated != null ) {
			this.onLateUpdated();
		}
	}

	void OnRenderObject()
	{
		//Debug.Log( Camera.current.projectionMatrix );
		//Matrix4x4 mat = Camera.current.projectionMatrix;
		//Debug.Log ( mat );
		//float rn = (-mat.m32 - mat.m22) / mat.m23;
		//float scale = rn / mat.m11;
		//Debug.Log( "znear:" + (1.0f / rn) + " rn:" + rn + " edge_scale:" + scale );
#if false
		_UpdatedDeffered();
		if( !Application.isPlaying ) {
			_UpdateAmbientPreview();
		}
#endif
	}

	void OnDestroy()
	{
		#if MMD4MECANIM_DEBUG
		Debug.Log("MMD4MecanimModel.OnDestroy() _bulletPhysicsMMDModel:" + (_bulletPhysicsMMDModel != null) );
		#endif

		_DestroyDeferredMaterial();
		_DestroyNEXTMaterial();

		if( this.ikList != null ) {
			for( int i = 0; i < this.ikList.Length; ++i ) {
				if( this.ikList[i] != null ) {
					this.ikList[i].Destroy();
				}
			}
			this.ikList = null;
		}

		_sortedBoneList = null;

		if( this.boneList != null ) {
			for( int i = 0; i < this.boneList.Length; ++i ) {
				if( this.boneList[i] != null ) {
					this.boneList[i].Destroy();
				}
			}
			this.boneList = null;
		}

		if( _bulletPhysicsMMDModel != null && !_bulletPhysicsMMDModel.isExpired ) {
			MMD4MecanimBulletPhysics instance = MMD4MecanimBulletPhysics.instance;
			if( instance != null ) {
				instance.DestroyMMDModel( _bulletPhysicsMMDModel );
			}
		}
		_bulletPhysicsMMDModel = null;
	}

	public void Initialize()
	{
		if( !Application.isPlaying ) {
			InitializeOnEditor();
			return;
		}

		if( this == null ) {
			#if MMD4MECANIM_DEBUG
			Debug.LogError("this is null.");
			#endif
			return;
		}

		if( _initialized ) {
			return;
		}

		#if MMD4MECANIM_DEBUG
		Debug.LogWarning("Initialize:" + this.name);
		#endif

		_initialized = true;
		_blendShapesEnabledCache = this.blendShapesEnabled;
		
		_InitializeMesh();
		_InitializeModel();
		_InitializeRigidBody();
		_PrepareBlendShapes();
		_InitializeBlendShapes();
		//_InitializeIndex();
		_InitializeAnimatoion();
		_InitializePhysicsEngine(); // Memo: Chained _InitializeCloneMesh() / Require _InitializeDeferredMesh
		_InitializePPHBones();
		_InitializeDeferredMesh();
		_InitializeNEXTEdgeMesh();
		_InitializeGlobalAmbient();
		_InitializeIK();
	}

	public AudioSource GetAudioSource()
	{
		if( this.audioSource == null ) {
			this.audioSource = this.gameObject.GetComponent< AudioSource >();
			if( this.audioSource == null ) {
				this.audioSource = this.gameObject.AddComponent< AudioSource >();
			}
		}

		return this.audioSource;
	}

	public void InitializeOnEditor()
	{
		if( this == null ) { // Bugfix: Lock inspector view.
			return;
		}

		if( _modelData == null ) {
			_initialized = false;
		}
		if( _modelData == null && this.modelFile == null ) {
			return;
		}

		if( _modelData == null ) {
			if( !_InitializeModelData() ) {
				return;
			}
		}
		
		if( _modelData != null ) {
			if( _modelData.boneDataList != null ) {
				if( this.boneList == null || this.boneList.Length != _modelData.boneDataList.Length ) {
					_initialized = false;
				}
			}
		}
		
		if( _initialized ) {
			return;
		}

		#if MMD4MECANIM_DEBUG
		Debug.LogWarning("Initialize:" + this.name);
		#endif

		_initialized = true;
		_blendShapesEnabledCache = blendShapesEnabled;

		_InitializeMesh();
		_InitializeModel();
		_InitializeRigidBody();
		_PrepareBlendShapes();
		_InitializeBlendShapes();
		_InitializeAnimatoion();
		//_InitializePhysicsEngine();
		//_InitializePPHBones();
		_InitializeDeferredMaterial(); // for Editor only.
		_InitializeNEXTMaterial(); // for Editor only.
		//_InitializeGlobalAmbient();
		_InitializeIK();
	}

	private void _InitializeMesh()
	{
		if( _meshRenderers != null ) {
			for( int i = 0; i != _meshRenderers.Length; ++i ) {
				if( _meshRenderers[i] == null ) {
					_meshRenderers = null;
					break;
				}
			}
		}
		if( _skinnedMeshRenderers != null ) {
			for( int i = 0; i != _skinnedMeshRenderers.Length; ++i ) {
				if( _skinnedMeshRenderers[i] == null ) {
					_skinnedMeshRenderers = null;
					break;
				}
			}
		}

		if( _meshRenderers == null || _meshRenderers.Length == 0 ) {
			_meshRenderers = MMD4MecanimCommon.GetMeshRenderers( this.gameObject );
			if( _meshRenderers == null ) {
				_meshRenderers = new MeshRenderer[0];
			}
		}
		if( _skinnedMeshRenderers == null || _skinnedMeshRenderers.Length == 0 ) {
			_skinnedMeshRenderers = MMD4MecanimCommon.GetSkinnedMeshRenderers( this.gameObject );
			if( _skinnedMeshRenderers == null ) {
				_skinnedMeshRenderers = new SkinnedMeshRenderer[0];
			}
			if( _skinnedMeshRenderers != null ) {
				for( int i = 0; i != _skinnedMeshRenderers.Length; ++i ) {
					if( _skinnedMeshRenderers[i].updateWhenOffscreen != this.updateWhenOffscreen ) {
						_skinnedMeshRenderers[i].updateWhenOffscreen = this.updateWhenOffscreen;
					}
				}
			}
		}

		if( this.defaultMesh == null ) {
			if( _skinnedMeshRenderers != null && _skinnedMeshRenderers.Length != 0 ) {
				this.defaultMesh = _skinnedMeshRenderers[0].sharedMesh;
			}
		}

		if( this.defaultMesh == null ) {
			if( _meshRenderers != null && _meshRenderers.Length != 0 ) {
				MeshFilter meshFilter = gameObject.GetComponent< MeshFilter >();
				if( meshFilter != null ) {
					this.defaultMesh = meshFilter.sharedMesh;
				}
			}
		}
	}

	void _PrepareBlendShapes()
	{
		#if UNITY_4_0 || UNITY_4_1 || UNITY_4_2
		// Not supported BlendShapes.
		#else
		// Memo: Not compare _blendShapesEnabledCache( Force reset always. )
		// Reset blendShapes.
		if( _skinnedMeshRenderers != null ) {
			for( int i = 0; i < _skinnedMeshRenderers.Length; ++i ) {
				SkinnedMeshRenderer skinnedMeshRenderer = _skinnedMeshRenderers[i];
				if( skinnedMeshRenderer != null ) {
					Mesh mesh = skinnedMeshRenderer.sharedMesh;
					if( mesh != null ) {
						for( int b = 0; b < mesh.blendShapeCount; ++b ) {
							if( Application.isPlaying ) {
								skinnedMeshRenderer.SetBlendShapeWeight( b, 0.0f );
							} else {
								if( skinnedMeshRenderer.GetBlendShapeWeight( b ) != 0.0f ) {
									skinnedMeshRenderer.SetBlendShapeWeight( b, 0.0f );
								}
							}
						}
					}
				}
			}
		}
		#endif
	}

	bool _IsBlendShapesAnything()
	{
		#if UNITY_4_0 || UNITY_4_1 || UNITY_4_2
		// Not supported BlendShapes.
		return false;
		#else
		if( _skinnedMeshRenderers != null ) {
			for( int i = 0; i < _skinnedMeshRenderers.Length; ++i ) {
				SkinnedMeshRenderer skinnedMeshRenderer = _skinnedMeshRenderers[i];
				if( skinnedMeshRenderer != null ) {
					Mesh mesh = skinnedMeshRenderer.sharedMesh;
					if( mesh != null && mesh.blendShapeCount > 0 ) {
						return true;
					}
				}
			}
		}

		return false;
		#endif
	}

	private void _InitializeBlendShapes()
	{
#if UNITY_4_0 || UNITY_4_1 || UNITY_4_2
		// Not supported BlendShapes.
#else
		if( _blendShapesEnabledCache &&
		    _skinnedMeshRenderers != null &&
		    _modelData != null &&
		    _modelData.morphDataList != null &&
		    _modelData.morphDataList.Length > 0 ) {
			if( _morphBlendShapes == null || _morphBlendShapes.Length != _skinnedMeshRenderers.Length ) {
				_morphBlendShapes = null;
				if( _IsBlendShapesAnything() ) {
					_morphBlendShapes = new MorphBlendShape[_skinnedMeshRenderers.Length];
					for( int i = 0; i < _skinnedMeshRenderers.Length; ++i ) {
						_morphBlendShapes[i] = new MorphBlendShape();
						_morphBlendShapes[i].blendShapeIndices = new int[_modelData.morphDataList.Length];
						for( int m = 0; m < _modelData.morphDataList.Length; ++m ) {
							_morphBlendShapes[i].blendShapeIndices[m] = -1;
						}
						SkinnedMeshRenderer skinnedMeshRenderer = _skinnedMeshRenderers[i];
						if( skinnedMeshRenderer.sharedMesh != null && skinnedMeshRenderer.sharedMesh.blendShapeCount > 0 ) {
							for( int b = 0; b < skinnedMeshRenderer.sharedMesh.blendShapeCount; ++b ) {
								string blendShapeName = skinnedMeshRenderer.sharedMesh.GetBlendShapeName( b );

								int morphID = -1;
								unchecked {
									if( MMD4MecanimCommon.IsID( blendShapeName ) ) {
										morphID = MMD4MecanimCommon.ToInt( blendShapeName );
									} else {
										morphID = _modelData.GetMorphDataIndex( blendShapeName, false );
									}
								}
								if( (uint)morphID < (uint)_modelData.morphDataList.Length ) {
									_morphBlendShapes[i].blendShapeIndices[morphID] = b;
								}
							}
						}
					}
				}
			}
		}
#endif
	}

	static void _PostfixRenderQueue(
		Material[] materials,
		bool postfixRenderQueue,
		bool renderQueueAfterSkybox )
	{
		if( Application.isPlaying ) { // Don't change renderQueue in Editor Mode.
			if( materials != null ) {
				for( int i = 0; i < materials.Length; ++i ) {
					if( materials[i].shader != null && materials[i].shader.name != null &&
						materials[i].shader.name.StartsWith("MMD4Mecanim") ) {
						if( postfixRenderQueue ) {
							int renderQueue = ( renderQueueAfterSkybox ? MaterialBaseRenderQueue_AfterSkybox : MaterialBaseRenderQueue );
							materials[i].renderQueue = renderQueue + MMD4MecanimCommon.ToInt( materials[i].name );
						}
					}
				}
			}
		}
	}

	private static void _SetupCloneMaterial( CloneMaterial cloneMaterial, Material[] materials )
	{
		cloneMaterial.materials = materials;
		if( materials != null ) {
			int materialLength = materials.Length;
			cloneMaterial.materialData = new MMD4MecanimData.MorphMaterialData[materialLength];
			cloneMaterial.backupMaterialData = new MMD4MecanimData.MorphMaterialData[materialLength];
			cloneMaterial.updateMaterialData = new bool[materialLength];
			for( int i = 0; i < materialLength; ++i ) {
				if( materials[i] != null ) {
					MMD4MecanimCommon.BackupMaterial( ref cloneMaterial.backupMaterialData[i], materials[i] );
					cloneMaterial.materialData[i] = cloneMaterial.backupMaterialData[i];
				}
			}
		}
	}

	public bool IsEnableMorphBlendShapes( int meshIndex )
	{
		if( _morphBlendShapes == null ) {
			return false;
		}
		if( meshIndex < 0 || meshIndex >= _morphBlendShapes.Length ) {
			return false;
		}
		if( _morphBlendShapes[meshIndex].blendShapeIndices == null ) {
			return false;
		}

		for( int i = 0; i != _morphBlendShapes[meshIndex].blendShapeIndices.Length; ++i ) {
			int index = _morphBlendShapes[meshIndex].blendShapeIndices[i];
			if( index != -1 ) {
				return true;
			}
		}

		return false;
	}

	bool _InitializeModelData()
	{
		if( this.modelFile == null ) {
			Debug.LogWarning( this.gameObject.name + ":modelFile is nothing." );
			return false;
		}
		
		_modelData = MMD4MecanimData.BuildModelData( this.modelFile );
		if( _modelData == null ) {
			Debug.LogError( this.gameObject.name + ":modelFile is unsupported format." );
			return false;
		}

		return true;
	}

	void _InitializeModel()
	{
		if( _modelData == null ) {
			if( !_InitializeModelData() ) {
				return;
			}
		}

		if( _modelData.boneDataList != null && _modelData.boneDataDictionary != null ) {
			if( this.boneList == null || this.boneList.Length != _modelData.boneDataList.Length ) {
				this.boneList = new Bone[_modelData.boneDataList.Length];
				_BindBone();

				// Bind(originalParent/target/child/inherenceParent)
				for( int i = 0; i < this.boneList.Length; ++i ) {
					if( this.boneList[i] != null ) {
						this.boneList[i].Bind();
					}
				}
				
				// sortedBoneList
				_sortedBoneList = new Bone[this.boneList.Length];
				for( int i = 0; i < this.boneList.Length; ++i ) {
					if( this.boneList[i] != null ) {
						BoneData boneData = this.boneList[i].boneData;
						if( boneData != null ) {
							int sortedBoneID = boneData.sortedBoneID;
							if( sortedBoneID >= 0 && sortedBoneID < this.boneList.Length ) {
								#if MMD4MECANIM_DEBUG
								if( _sortedBoneList[sortedBoneID] != null ) { // Check overwrite.
									Debug.LogError("");
								}
								#endif
								_sortedBoneList[sortedBoneID] = this.boneList[i];
							} else {
								#if MMD4MECANIM_DEBUG
								Debug.LogError("");
								#endif
							}
						}
					}
				}
			}
		}

		// ikList
		if( _modelData.ikDataList != null ) {
			int ikListLength = _modelData.ikDataList.Length;
			this.ikList = new IK[ikListLength];
			for( int i = 0; i < ikListLength; ++i ) {
				this.ikList[i] = new IK( this, i );
			}
		}

		// morphList
		if( _modelData.morphDataList != null ) {
			this.morphList = new MMD4MecanimModel.Morph[_modelData.morphDataList.Length];
			for( int i = 0; i < _modelData.morphDataList.Length; ++i ) {
				this.morphList[i] = new Morph();
				this.morphList[i].morphData = _modelData.morphDataList[i];

				// for AutoLuminous
				string morphName = this.morphList[i].name;
				if( !string.IsNullOrEmpty(morphName) ) {
					switch( morphName ) {
					case "LightUp":
						this.morphList[i].morphAutoLuminousType = MorphAutoLumninousType.LightUp;
						break;
					case "LightOff":
						this.morphList[i].morphAutoLuminousType = MorphAutoLumninousType.LightOff;
						break;
					case "LightBlink":
						this.morphList[i].morphAutoLuminousType = MorphAutoLumninousType.LightBlink;
						break;
					case "LightBS":
						this.morphList[i].morphAutoLuminousType = MorphAutoLumninousType.LightBS;
						break;
					}
				}
			}
		}

		// morphAutoLuminous
		this.morphAutoLuminous = new MorphAutoLuminous();
	}
}
