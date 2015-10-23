using UnityEngine;
using System.Collections;

public class ForwardRoll : StateMachineBehaviour {
	public static int F = 0;
	public static int O = 0;
	public static int R = 0;
	public static int Ward = 0;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if (Get_Acceleration.standflag == 1 && Get_Acceleration.reverseflag == 0 && Get_Acceleration.rightflag == 0 &&
		    Get_Acceleration.leftflag == 0 && Get_Acceleration.supineflag == 0 && Get_Acceleration.proneflag == 0) {
			F = 1;
		}
		
		if (Get_Acceleration.standflag == 1 && Get_Acceleration.reverseflag == 0 && Get_Acceleration.rightflag == 0 &&
		    Get_Acceleration.leftflag == 0 && Get_Acceleration.supineflag == 0 && Get_Acceleration.proneflag == 6) {
			O = 1;
		}
		
		if (Get_Acceleration.standflag == 1 && Get_Acceleration.reverseflag == 2 && Get_Acceleration.rightflag == 0 &&
		    Get_Acceleration.leftflag == 0 && Get_Acceleration.supineflag == 0 && Get_Acceleration.proneflag == 6) {
			R = 1;
		}
		
		if (Get_Acceleration.standflag == 1 && Get_Acceleration.reverseflag == 2 && Get_Acceleration.rightflag == 0 &&
		    Get_Acceleration.leftflag == 0 && Get_Acceleration.supineflag == 5 && Get_Acceleration.proneflag == 6) {
			Ward = 1;
		}
		if(F == 1 && O == 1 && R == 1 && Ward ==1){
			Get_Acceleration.anim.SetTrigger("ForwardRoll");
			StateToText.Zenten();
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
