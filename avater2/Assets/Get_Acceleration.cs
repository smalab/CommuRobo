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
	public static int flag = 0;

	void Start () {
		anim = GetComponent<Animator> ();

	}
	
	// Update is called once per frame
	void Update () {
		Vector3 acceleration = Input.acceleration;
		Debug.Log (acceleration);
/*		Debug.Log ("flag no joukyou");
		Debug.Log(standflag);
		Debug.Log(reverseflag);
		Debug.Log(rightflag);
		Debug.Log(leftflag);
		Debug.Log(supineflag);
		Debug.Log(proneflag);*/


		//-0.9~-1.3 no aida
		if (acceleration.x <= 0.1 && acceleration.y <= -0.9  && acceleration.y >= -1.3) {
			State_Information.SetStand();
			standflag = 1;
			statenum = 1;

		}
		if (acceleration.x >= -0.1 && acceleration.x <= 0.1 && acceleration.y >= 0.8 && acceleration.y <= 1.1 && acceleration.z <= 0.2) {
			State_Information.SetReverse();
			reverseflag = 2;
			statenum = 2;
		}
		if (acceleration.x >= 0.8 && acceleration.x <= 1.0 && acceleration.y >= -0.7 && acceleration.y <= 0.2) {
			State_Information.SetRight();
			rightflag = 3;
			statenum = 3;
		}
		if (acceleration.x <= -0.8 && acceleration.x >= -1.0 && acceleration.y >= -0.7 && acceleration.y <= 0.2) {
			State_Information.SetLeft();
			leftflag = 4;
			statenum = 4;
		}
		if (acceleration.z <= -0.95) {
			State_Information.SetSupine();
			supineflag = 5;
			statenum = 5;
		}
		if (acceleration.z >= 0.95) {
			State_Information.SetProne();
			proneflag = 6;
			statenum = 6;
		}

		//////////////////undoujoutai////////////////////////////////

		if (acceleration.y <= -1.8) {
			State_Information.SetDown();
			flag = 0;
			StateToText.Down();
		}

		if (standflag == 1 && reverseflag == 0 && rightflag == 0 && leftflag == 0 && supineflag == 0 && proneflag == 0) {
			if (acceleration.y >= -0.5 && acceleration.z <= 0.3 && acceleration.z >= -0.3) {
				State_Information.SetUp ();
				flag = 1;
				StateToText.Up ();
			}
		}
		if (flag == 1 && Input.gyro.rotationRateUnbiased.y > 1.8) {
			StateToText.Ayasu();
		}
		
	}






}
