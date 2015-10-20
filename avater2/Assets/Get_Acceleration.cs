using UnityEngine;
using System.Collections;

public class Get_Acceleration : MonoBehaviour {
	// Use this for initialization

	public static Animator anim;
	void Start () {
		anim = GetComponent<Animator> ();

	}
	
	// Update is called once per frame
	void Update () {
		Vector3 acceleration = Input.acceleration;
		Debug.Log (acceleration);

		if (acceleration.x <= 0.2 && acceleration.y <= -0.9) {
			SetStand();
		}
		if (acceleration.x >= -0.2 && acceleration.x <= 0.2 && acceleration.y >= 0.8 && acceleration.y <= 1.1 && acceleration.z <= 0.2) {
			SetReverse();
		}
		if (acceleration.x >= 0.8 && acceleration.x <= 1.0 && acceleration.y >= -0.7 && acceleration.y <= 0.2) {
			SetRight();
		}
		if (acceleration.x <= -0.8 && acceleration.x >= -1.0 && acceleration.y >= -0.7 && acceleration.y <= 0.2) {
			SetLeft();
		}
		if (acceleration.z <= -0.95) {
			SetSupine();
		}
		if (acceleration.z >= 0.95) {
			SetProne();
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
	public static void ResetTrigger(){
		anim.ResetTrigger ("stand");
		anim.ResetTrigger ("reverse");
		anim.ResetTrigger ("right");
		anim.ResetTrigger ("left");
		anim.ResetTrigger ("supine");
		anim.ResetTrigger ("prone");
	}

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
