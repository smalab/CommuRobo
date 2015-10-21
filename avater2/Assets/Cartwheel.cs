using UnityEngine;
using System.Collections;

public class Cartwheel : StateMachineBehaviour {
	int flagC;
	int flagA;
	int flagR;
	int flagT;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	if (Get_Acceleration.standflag == 1 && Get_Acceleration.reverseflag == 0 && Get_Acceleration.rightflag == 0 &&
	    Get_Acceleration.leftflag == 0 && Get_Acceleration.supineflag == 0 && Get_Acceleration.proneflag == 0) {
		flagC = 1;
	}
	
	if (Get_Acceleration.standflag == 1 && Get_Acceleration.reverseflag == 0 && Get_Acceleration.rightflag == 3 &&
	    Get_Acceleration.leftflag == 0 && Get_Acceleration.supineflag == 0 && Get_Acceleration.proneflag == 0) {
		flagA = 1;
	}
	
	if (Get_Acceleration.standflag == 1 && Get_Acceleration.reverseflag == 2 && Get_Acceleration.rightflag == 3 &&
	    Get_Acceleration.leftflag == 0 && Get_Acceleration.supineflag == 0 && Get_Acceleration.proneflag == 0) {
		flagR = 1;
	}
	
	if (Get_Acceleration.standflag == 1 && Get_Acceleration.reverseflag == 2 && Get_Acceleration.rightflag == 3 &&
	    Get_Acceleration.leftflag == 4 && Get_Acceleration.supineflag == 0 && Get_Acceleration.proneflag == 0) {
		flagT = 1;
	}
	if(flagC == 1 && flagA == 1 && flagR == 1 && flagT ==1){
		Get_Acceleration.anim.SetTrigger("Cartwheel");
		StateToText.Sokuten();
	}	
}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	//override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
