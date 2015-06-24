﻿//#define MMD4MECANIM_ENABLE_DEFFERED

using UnityEngine;
using System.Collections;

public partial class MMD4MecanimModel
{
	#if MMD4MECANIM_ENABLE_DEFFERED
	public Material			deferredZClearMaterial;
	public Material			dummyMaterial;

	MeshRenderer[]			_deferredMeshRenderers;
	SkinnedMeshRenderer[]	_deferredSkinnedMeshRenderers;

	bool					_setDeferredAtLeastOnce;
	Color					_lastDeferredLightColor0;
	Vector4					_lastDeferredLightDir0;
	#endif

	void _InitializeDeferredMaterial()
	{
		#if MMD4MECANIM_ENABLE_DEFFERED
		if( this.deferredZClearMaterial == null ) {
			Shader shader = Shader.Find("MMD4Mecanim/MMDLit-Deferred-ZClear");
			if( shader != null ) {
				this.deferredZClearMaterial = new Material( shader );
			}
		}
		if( this.dummyMaterial == null ) {
			Shader shader = Shader.Find("MMD4Mecanim/MMDLit-Dummy");
			if( shader != null ) {
				this.dummyMaterial = new Material( shader );
			}
		}
		#endif
	}

	void _DestroyDeferredMaterial()
	{
		#if MMD4MECANIM_ENABLE_DEFFERED
		if( this.deferredZClearMaterial != null ) {
			if( Application.isPlaying ) {
				Material.Destroy( this.deferredZClearMaterial );
			} else {
				Material.DestroyImmediate( this.deferredZClearMaterial );
			}
			this.deferredZClearMaterial = null;
		}
		if( this.dummyMaterial != null ) {
			if( Application.isPlaying ) {
				Material.Destroy( this.dummyMaterial );
			} else {
				Material.DestroyImmediate( this.dummyMaterial );
			}
			this.dummyMaterial = null;
		}
		#endif
	}

	void _InitializeDeferredMesh()
	{
		#if MMD4MECANIM_ENABLE_DEFFERED
		if( !_supportDeferred ) {
			return;
		}

		_InitializeDeferredMaterial();

		if( this.deferredZClearMaterial == null || this.dummyMaterial == null ) {
			Debug.LogWarning( "deferredZClearMaterial / dummyMaterial is nothing. Skipped _InitializeDeferredMesh()." );
			return;
		}

		if( _meshRenderers != null ) {
			_deferredMeshRenderers = new MeshRenderer[_meshRenderers.Length];
			for( int i = 0; i < _meshRenderers.Length; ++i ) {
				MeshRenderer meshRenderer = _meshRenderers[i];
				if( meshRenderer != null ) {
					Material[] materials = meshRenderer.sharedMaterials;
					if( materials == null || materials.Length == 0 ) {
						materials = meshRenderer.materials;
					}
					if( materials != null ) {
						materials = _CloneDeferredClearMaterials( materials );
					}
					if( materials != null ) {
						GameObject go = _CreateDeferrecClearGameObject( meshRenderer.gameObject );
						MeshRenderer r = go.AddComponent<MeshRenderer>();
						#if UNITY_4_0 || UNITY_4_1 || UNITY_4_2 || UNITY_4_3 || UNITY_4_4 || UNITY_4_5 || UNITY_4_6
						r.castShadows = false;
						#else
						r.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
						#endif
						r.receiveShadows = false;
						r.materials = materials;
						MeshFilter meshFilter = _meshRenderers[i].gameObject.GetComponent<MeshFilter>();
						if( meshFilter != null ) {
							MeshFilter m = go.AddComponent<MeshFilter>();
							m.sharedMesh = meshFilter.sharedMesh;
						}
						_deferredMeshRenderers[i] = r;
					}
				}
			}
		}
		
		if( _skinnedMeshRenderers != null ) {
			_deferredSkinnedMeshRenderers = new SkinnedMeshRenderer[_skinnedMeshRenderers.Length];
			for( int i = 0; i < _skinnedMeshRenderers.Length; ++i ) {
				SkinnedMeshRenderer skinnedMeshRenderer = _skinnedMeshRenderers[i];
				if( skinnedMeshRenderer != null ) {
					Material[] materials = skinnedMeshRenderer.sharedMaterials;
					if( materials == null || materials.Length == 0 ) {
						materials = skinnedMeshRenderer.materials;
					}
					if( materials != null ) {
						materials = _CloneDeferredClearMaterials( materials );
					}
					if( materials != null ) {
						GameObject go = _CreateDeferrecClearGameObject( skinnedMeshRenderer.gameObject );
						SkinnedMeshRenderer r = go.AddComponent<SkinnedMeshRenderer>();
						r.sharedMesh = skinnedMeshRenderer.sharedMesh;
						r.bones = skinnedMeshRenderer.bones;
						r.rootBone = skinnedMeshRenderer.rootBone;
						#if UNITY_4_0 || UNITY_4_1 || UNITY_4_2 || UNITY_4_3 || UNITY_4_4 || UNITY_4_5 || UNITY_4_6
						r.castShadows = false;
						#else
						r.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
						#endif
						r.receiveShadows = false;
						r.materials = materials;
						_deferredSkinnedMeshRenderers[i] = r;
					}
				}
			}
		}
		#endif
	}

	static GameObject _CreateDeferrecClearGameObject( GameObject parentGameObject )
	{
		#if MMD4MECANIM_ENABLE_DEFFERED
		if( parentGameObject != null ) {
			GameObject go = new GameObject(parentGameObject.name + "(Deferred)");
			go.transform.parent = parentGameObject.transform;
			go.transform.localPosition = Vector3.zero;
			go.transform.localRotation = Quaternion.identity;
			go.transform.localScale = Vector3.one;
			return go;
		}
		#endif
		return null;
	}

	Material[] _CloneDeferredClearMaterials( Material[] materials )
	{
		#if MMD4MECANIM_ENABLE_DEFFERED
		if( materials != null ) {
			bool deferredZClearAnything = false;
			Material[] m = new Material[materials.Length];
			for( int j = 0; j < materials.Length; ++j ) {
				if( materials[j] != null && materials[j].shader != null ) {
					if( materials[j].shader.name.Contains("Transparent") ) {
						deferredZClearAnything = true;
						m[j] = this.deferredZClearMaterial;
					} else {
						m[j] = this.dummyMaterial;
					}
				}
			}
			if( deferredZClearAnything ) {
				return m;
			}
		}
		#endif
		return null;
	}

	void _UpdatedDeffered()
	{
		#if MMD4MECANIM_ENABLE_DEFFERED
		if( !_supportDeferred ) {
			return;
		}

		_deferredLight = null;
		Light[] lights = FindObjectsOfType( typeof(Light) ) as Light[];
		if( lights != null ) {
			foreach( Light light in lights ) {
				if( light.type == LightType.Directional && light.enabled && light.gameObject.activeSelf ) {
					if( _deferredLight == null ) {
						_deferredLight = light;
					} else {
						if( _deferredLight.renderMode == light.renderMode ) {
							if( _deferredLight.intensity < light.intensity ) {
								_deferredLight = light;
							}
						} else if( light.renderMode == LightRenderMode.ForcePixel ) {
							_deferredLight = light;
						}
					}
				}
			}
		}

		_SetDeferredShaderSettings( _deferredLight );
		#endif
	}
	
	void _SetDeferredShaderSettings( Light directionalLight )
	{
		#if MMD4MECANIM_ENABLE_DEFFERED
		Color defLightColor0 = Color.black;
		Vector4 defLightDir0 = new Vector4( 0.0f, 0.0f, 1.0f, 1.0f );
		if( directionalLight != null ) {
			defLightColor0 = directionalLight.color * directionalLight.intensity * 2.0f;
			Matrix4x4 lightMat = directionalLight.gameObject.transform.localToWorldMatrix;
			defLightDir0.x = -lightMat.m02;
			defLightDir0.y = -lightMat.m12;
			defLightDir0.z = -lightMat.m22;
		}

		if( Application.isPlaying ) {
			if( !_setDeferredAtLeastOnce ) {
				_setDeferredAtLeastOnce = true;
				_lastDeferredLightColor0 = defLightColor0;
				_lastDeferredLightDir0 = defLightDir0;
			} else {
				if( _lastDeferredLightColor0 == defLightColor0 &&
				    _lastDeferredLightDir0 == defLightDir0 ) {
					return;
				}
				_lastDeferredLightColor0 = defLightColor0;
				_lastDeferredLightDir0 = defLightDir0;
			}
		}

		if( _cloneMaterials != null ) {
			for( int i = 0; i < _cloneMaterials.Length; ++i ) {
				Material[] materials = _cloneMaterials[i].materials;
				if( materials != null ) {
					for( int m = 0; m < materials.Length; ++m ) {
						Material material = _cloneMaterials[i].materials[m];
						if( material != null && MMD4MecanimCommon.IsDeferredShader( material ) ) {
							if( Application.isPlaying ) {
								material.SetColor( "_DefLightColor0", defLightColor0 );
								material.SetVector( "_DefLightDir0", defLightDir0 );
							} else {
								MMD4MecanimCommon.WeakSetMaterialColor( material, "_DefLightColor0", defLightColor0 );
								MMD4MecanimCommon.WeakSetMaterialVector( material, "_DefLightDir0", defLightDir0 );
							}
						}
					}
				}
			}
		}
		#endif
	}
}
