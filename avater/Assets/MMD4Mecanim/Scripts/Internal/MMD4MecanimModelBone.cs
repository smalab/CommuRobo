using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using MorphCategory		= MMD4MecanimData.MorphCategory;
using MorphType			= MMD4MecanimData.MorphType;
using MorphData			= MMD4MecanimData.MorphData;
using MorphMotionData	= MMD4MecanimData.MorphMotionData;
using BoneData			= MMD4MecanimData.BoneData;
using FileType			= MMD4MecanimData.FileType;
using PMDBoneType		= MMD4MecanimData.PMDBoneType;
using PMXBoneFlags		= MMD4MecanimData.PMXBoneFlags;

// pending: Inherence RigidBody support
// pending: Transform after physics support

public partial class MMD4MecanimModel
{
	// from _InitializeModel()
	void _BindBone()
	{
		Transform transform = this.gameObject.transform;
		foreach( Transform trn in transform ) {
			_BindBone( trn );
		}
	}
	
	void _BindBone( Transform trn )
	{
		if( !string.IsNullOrEmpty( trn.gameObject.name ) ) {
			int boneID = 0;
			if( _modelData.boneDataDictionary.TryGetValue( trn.gameObject.name, out boneID ) ) {
				MMD4MecanimBone bone = trn.gameObject.GetComponent< MMD4MecanimBone >();
				if( bone == null ) {
					bone = trn.gameObject.AddComponent< MMD4MecanimBone >();
				}
				bone.model = this;
				bone.boneID = boneID;
				bone.Setup();
				#if MMD4MECANIM_DEBUG
				//Debug.LogWarning( "Setup boneID:" + trn.gameObject.name + " this:" + (this != null) + " bone.model:" + (bone.model != null) );
				#endif
				this.boneList[boneID] = bone;
				if( this.boneList[boneID].boneData != null && this.boneList[boneID].boneData.isRootBone ) {
					_rootBone = this.boneList[boneID];
				}
			} else {
				#if MMD4MECANIM_DEBUG
				//Debug.LogWarning( "Not found boneID:" + trn.gameObject.name );
				#endif
			}
		}
		foreach( Transform t in trn ) {
			_BindBone( t );
		}
	}
	
	//--------------------------------------------------------------------------------------------------------------------------------------------

	void _UpdateBone()
	{
		// Nothing.
	}

	void _LateUpdateBone()
	{
		_UpdatePPHBones();
	}

	//--------------------------------------------------------------------------------------------------------------------------------------------

	MMD4MecanimBone _leftShoulderBone;
	MMD4MecanimBone _rightShoulderBone;
	MMD4MecanimBone _leftArmBone;
	MMD4MecanimBone _rightArmBone;

	void _InitializePPHBones()
	{
		if( _animator == null || _animator.avatar == null || !_animator.avatar.isValid || !_animator.avatar.isHuman ) {
			return;
		}
		{
			Transform leftShoulderTransform = _animator.GetBoneTransform(HumanBodyBones.LeftShoulder);
			Transform leftArmTransform = _animator.GetBoneTransform(HumanBodyBones.LeftUpperArm);
			if( leftShoulderTransform != null && leftArmTransform != null ) {
				_leftShoulderBone = leftShoulderTransform.gameObject.GetComponent< MMD4MecanimBone >();
				_leftArmBone = leftArmTransform.gameObject.GetComponent< MMD4MecanimBone >();
				if( _leftShoulderBone != null ) {
					_leftShoulderBone.humanBodyBones = (int)HumanBodyBones.LeftShoulder;
				}
				if( _leftArmBone != null ) {
					_leftArmBone.humanBodyBones = (int)HumanBodyBones.LeftUpperArm;
				}
			}
		}
		{
			Transform rightShoulderTransform = _animator.GetBoneTransform(HumanBodyBones.RightShoulder);
			Transform rightArmTransform = _animator.GetBoneTransform(HumanBodyBones.RightUpperArm);
			if( rightShoulderTransform != null && rightArmTransform != null ) {
				_rightShoulderBone = rightShoulderTransform.gameObject.GetComponent< MMD4MecanimBone >();
				_rightArmBone = rightArmTransform.gameObject.GetComponent< MMD4MecanimBone >();
				if( _rightShoulderBone != null ) {
					_rightShoulderBone.humanBodyBones = (int)HumanBodyBones.RightShoulder;
				}
				if( _rightArmBone != null ) {
					_rightArmBone.humanBodyBones = (int)HumanBodyBones.RightUpperArm;
				}
			}
		}
	}

	public void ForceUpdatePPHBones()
	{
		_UpdatePPHBones();
	}

	void _UpdatePPHBones()
	{
		_pphShoulderFixRateImmediately = 0.0f;

		if( !this.pphEnabled ) {
			return;
		}
		if( _animator == null ) {
			return;
		}

		bool isNoAnimation = false;
		#if UNITY_4_0 || UNITY_4_1 || UNITY_4_2 || UNITY_4_3 || UNITY_4_4 || UNITY_4_5 || UNITY_4_6
		var animationInfos = _animator.GetCurrentAnimationClipState(0);
		#else
		var animationInfos = _animator.GetCurrentAnimatorClipInfo(0);
		#endif
		if( animationInfos == null || animationInfos.Length == 0 ) {
			isNoAnimation = true;
			if( !this.pphEnabledNoAnimation ) {
				return; // No playing animation.
			}
		}

		float pphRate = 0.0f;
		if( isNoAnimation ) {
			pphRate = 1.0f; // pphEnabledNoAnimation
		} else {
			for( int i = 0; i != animationInfos.Length; ++i ) {
				var animationInfo = animationInfos[i];
				if( animationInfo.clip != null &&
					animationInfo.clip.name != null &&
					!animationInfo.clip.name.EndsWith( ".vmd" ) ) {
					pphRate += animationInfo.weight;
				}
			}
			if( pphRate <= Mathf.Epsilon ) {
				return;
			}
		}
		
		_pphShoulderFixRateImmediately = this.pphShoulderFixRate * pphRate;
	}
}
