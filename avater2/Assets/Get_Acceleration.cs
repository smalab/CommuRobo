using UnityEngine;
using System.Collections;

public class Get_Acceleration : MonoBehaviour {
	// Use this for initialization

	public static Animator anim;

	//kakuninn you
	public static int standflag = 0;
	public static int reverseflag = 0;
	public static int rightflag = 0;
	public static int leftflag = 0;
	public static int supineflag = 0;
	public static int proneflag = 0;
	//kokomade


	//public static int[] statearray = new int[4];
	public static int statenum = 0;


	void Start () {
		anim = GetComponent<Animator> ();

	}
	
	// Update is called once per frame
	void Update () {
		Vector3 acceleration = Input.acceleration;
		//Debug.Log (acceleration);
		Debug.Log(standflag);
		Debug.Log(reverseflag);
		Debug.Log(rightflag);
		Debug.Log(leftflag);
		Debug.Log(supineflag);
		Debug.Log(proneflag);

		if (acceleration.x <= 0.1 && acceleration.y <= -0.9) {
			SetStand();
			standflag = 1;
			statenum = 1;

		}
		if (acceleration.x >= -0.1 && acceleration.x <= 0.1 && acceleration.y >= 0.8 && acceleration.y <= 1.1 && acceleration.z <= 0.2) {
			SetReverse();
			reverseflag = 2;
			statenum = 2;
		}
		if (acceleration.x >= 0.8 && acceleration.x <= 1.0 && acceleration.y >= -0.7 && acceleration.y <= 0.2) {
			SetRight();
			rightflag = 3;
			statenum = 3;
		}
		if (acceleration.x <= -0.8 && acceleration.x >= -1.0 && acceleration.y >= -0.7 && acceleration.y <= 0.2) {
			SetLeft();
			leftflag = 4;
			statenum = 4;
		}
		if (acceleration.z <= -0.95) {
			SetSupine();
			supineflag = 5;
			statenum = 5;
		}
		if (acceleration.z >= 0.95) {
			SetProne();
			proneflag = 6;
			statenum = 6;
		}
	
	}

	public void SetStand(){
		GetComponent<Animator>().SetTrigger("stand");
	}
	public void SetReverse(){
		GetComponent<Animator>().SetTrigger("reverse");
	}
	public void SetRight(){
		GetComponent<Animator>().SetTrigger("right");
	}
	public void SetLeft(){
		GetComponent<Animator>().SetTrigger("left");
	}
	public void SetSupine(){
		GetComponent<Animator>().SetTrigger("supine");
	}
	public void SetProne(){
		GetComponent<Animator>().SetTrigger("prone");
	}

	public static void SetSleep(){
		anim.SetTrigger("Sleeping");
	}
	public static void ResetTrigger(){
		anim.ResetTrigger ("stand");
		anim.ResetTrigger ("reverse");
		anim.ResetTrigger ("right");
		anim.ResetTrigger ("left");
		anim.ResetTrigger ("supine");
		anim.ResetTrigger ("prone");

	}
	public static void ResetFrag(){
		standflag = 0;
		reverseflag = 0;
		rightflag = 0;
		leftflag = 0;
		supineflag = 0;
		proneflag = 0;
		PostureStatus.postureString = "Default";
	}
	/*public static void ResetArray(){
		int i;
		for (i=0; i<4; i++) {
			statearray [i] = 0;
		}

	}*/
	public static void ResetStand(){
		anim.ResetTrigger ("stand");
	}
	public static void ResetReverse(){
		anim.ResetTrigger ("reverse");
	}
	public static void ResetRight(){
		anim.ResetTrigger ("right");
	}
	public static void ResetLeft(){
		anim.ResetTrigger ("left");
	}
	public static void ResetSupine(){
		anim.ResetTrigger ("supine");
	}
	public static void ResetProne(){
		anim.ResetTrigger ("prone");
	}


}
