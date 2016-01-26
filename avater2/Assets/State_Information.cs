using UnityEngine;
using System.Collections;

public class State_Information : MonoBehaviour {
// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	public static void ResetTrigger(){
		Get_Acceleration.anim.ResetTrigger ("stand");
		Get_Acceleration.anim.ResetTrigger ("reverse");
		Get_Acceleration.anim.ResetTrigger ("right");
		Get_Acceleration.anim.ResetTrigger ("left");
		Get_Acceleration.anim.ResetTrigger ("supine");
		Get_Acceleration.anim.ResetTrigger ("prone");
		//Get_Acceleration.anim.ResetTrigger ("Up");
		//Get_Acceleration.anim.ResetTrigger ("Down");
	}
	public static void ResetFrag(){
		Get_Acceleration.standflag = 0;
		Get_Acceleration.reverseflag = 0;
		Get_Acceleration.rightflag = 0;
		Get_Acceleration.leftflag = 0;
		Get_Acceleration.supineflag = 0;
		Get_Acceleration.proneflag = 0;
		//PostureStatus.postureString = "Default";
	}


	public static void ResetPostureTrigger(){
		Get_Acceleration.anim.ResetTrigger ("stand");
		Get_Acceleration.anim.ResetTrigger ("reverse");
		Get_Acceleration.anim.ResetTrigger ("right");
		Get_Acceleration.anim.ResetTrigger ("left");
		Get_Acceleration.anim.ResetTrigger ("supine");
		Get_Acceleration.anim.ResetTrigger ("prone");
		Get_Acceleration.anim.ResetTrigger("BackRoll");
		Get_Acceleration.anim.ResetTrigger("CartWheel");
		Get_Acceleration.anim.ResetTrigger("ForwardRoll");
	}




	public static void ResetStand(){
		Get_Acceleration.anim.ResetTrigger ("stand");
	}
	public static void ResetReverse(){
		Get_Acceleration.anim.ResetTrigger ("reverse");
	}
	public static void ResetRight(){
		Get_Acceleration.anim.ResetTrigger ("right");
	}
	public static void ResetLeft(){
		Get_Acceleration.anim.ResetTrigger ("left");
	}
	public static void ResetSupine(){
		Get_Acceleration.anim.ResetTrigger ("supine");
	}
	public static void ResetProne(){
		Get_Acceleration.anim.ResetTrigger ("prone");
	}
	public static void ResetDown(){
		Get_Acceleration.anim.ResetTrigger ("Down");
	}
	public static void ResetUp(){
		Get_Acceleration.anim.ResetTrigger ("Up");
	}


	public static void SetStand(){
		Get_Acceleration.anim.SetTrigger("stand");
	}
	public static void SetReverse(){
		Get_Acceleration.anim.SetTrigger("reverse");
	}
	public static void SetRight(){
		Get_Acceleration.anim.SetTrigger("right");
	}
	public static void SetLeft(){
		Get_Acceleration.anim.SetTrigger("left");
	}
	public static void SetSupine(){
		Get_Acceleration.anim.SetTrigger("supine");
	}
	public static void SetProne(){
		Get_Acceleration.anim.SetTrigger("prone");
	}
	public static void SetSleep(){
		Get_Acceleration.anim.SetTrigger("Sleeping");
	}

/*	public static void SetUp(){
		Get_Acceleration.anim.SetTrigger("Up");
	}
	public static void SetDown(){
		Get_Acceleration.anim.SetTrigger("Down");
	}                                              */



}
