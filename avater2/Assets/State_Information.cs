using UnityEngine;
using System.Collections;

public class State_Information : MonoBehaviour {
	public AnimatorStateInfo stateInfo1;
	public AnimatorStateInfo stateInfo2;

/*	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}    */

	public void StateInfo(){
		// animator no info get
		AnimatorStateInfo stateInfo = Get_Acceleration.anim.GetCurrentAnimatorStateInfo (0);

	}
	public static void  Backten(){

		PostureStatus.postureString = "Backten";
	}
	


}
