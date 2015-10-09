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
		if (acceleration.x >= 0.4 && acceleration.x <= 0.9 && acceleration.y >= -0.7 && acceleration.y <= 0.2) {
			SetRight();
		}
		if (acceleration.x <= -0.4 && acceleration.x >= -0.9 && acceleration.y >= -0.7 && acceleration.y <= 0.2) {
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
		/*this.GetComponent<Animator> ().ResetTrigger ("stand");
		this.GetComponent<Animator> ().ResetTrigger ("reverse");
		this.GetComponent<Animator> ().ResetTrigger ("right");
		this.GetComponent<Animator> ().ResetTrigger ("left");
		this.GetComponent<Animator> ().ResetTrigger ("supine");
		this.GetComponent<Animator> ().ResetTrigger ("prone");*/
	}
}
