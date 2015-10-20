using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BackRoll: StateMachineBehaviour {
	public static AnimatorStateInfo stateInfo1;
 	public static AnimatorStateInfo stateInfo2;


	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		stateInfo1 = Get_Acceleration.anim.GetCurrentAnimatorStateInfo (0);


		//stateInfo2 = Get_Acceleration.anim.GetCurrentAnimatorStateInfo (0);



	/*	if(stateInfo1.IsName("Base Layer.Stand")  && stateInfo2.IsName("Base Layer.Supine")){

			Get_Acceleration.anim.SetTrigger("flagA");
		}
		if(stateInfo1.IsName("Base Layer.Supine") && stateInfo2.IsName("Base Layer.Reverse")){
			Get_Acceleration.anim.SetTrigger("flagB");
		}
		if(stateInfo1.IsName("Base Layer.Reverse")  && stateInfo2.IsName("Base Layer.Prone")){
			Get_Acceleration.anim.SetTrigger("flagC");
		}
		if(stateInfo1.IsName("Base Layer.Prone")  && stateInfo2.IsName("Base Layer.Stand")){
			Get_Acceleration.anim.SetTrigger("flagD");
		}



		stateInfo1 = stateInfo2;*/
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	//override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//	stateInfo1 = stateInfo2;
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
