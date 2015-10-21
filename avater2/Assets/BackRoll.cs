using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BackRoll: StateMachineBehaviour {
//	public static AnimatorStateInfo stateInfo1;
//	public static AnimatorStateInfo stateInfo2;

	public static int flagB = 0;
	public static int flagA = 0;
	public static int flagC = 0;
	public static int flagK = 0;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if (Get_Acceleration.standflag == 1 && Get_Acceleration.reverseflag == 0 && Get_Acceleration.rightflag == 0 &&
			Get_Acceleration.leftflag == 0 && Get_Acceleration.supineflag == 0 && Get_Acceleration.proneflag == 0) {
			flagB = 1;
		}

		if (Get_Acceleration.standflag == 1 && Get_Acceleration.reverseflag == 0 && Get_Acceleration.rightflag == 0 &&
		    Get_Acceleration.leftflag == 0 && Get_Acceleration.supineflag == 5 && Get_Acceleration.proneflag == 0) {
			flagA = 1;
		}

		if (Get_Acceleration.standflag == 1 && Get_Acceleration.reverseflag == 2 && Get_Acceleration.rightflag == 0 &&
		    Get_Acceleration.leftflag == 0 && Get_Acceleration.supineflag == 5 && Get_Acceleration.proneflag == 0) {
			flagC = 1;
		}

		if (Get_Acceleration.standflag == 1 && Get_Acceleration.reverseflag == 2 && Get_Acceleration.rightflag == 0 &&
		    Get_Acceleration.leftflag == 0 && Get_Acceleration.supineflag == 5 && Get_Acceleration.proneflag == 6) {
			flagK = 1;
		}
		if(flagB == 1 && flagA == 1 && flagC == 1 && flagK ==1){
			Get_Acceleration.anim.SetTrigger("BackRoll");
			StateToText.Backten();
		}


	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	//override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//}

	//OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

	//}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
/*	override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if(stateInfo1.IsName("Base Layer.Stand")  && stateInfo2.IsName("Base Layer.Supine")){
			flag1 = true;
		}
		if(stateInfo1.IsName("Base Layer.Supine") && stateInfo2.IsName("Base Layer.Reverse")){
			flag2 = true;
		}
		if(stateInfo1.IsName("Base Layer.Reverse")  && stateInfo2.IsName("Base Layer.Prone")){
			flag3 = true;
		}
		if(stateInfo1.IsName("Base Layer.Prone")  && stateInfo2.IsName("Base Layer.Stand")){
			flag4 = true;
		}
	}*/

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}


}
