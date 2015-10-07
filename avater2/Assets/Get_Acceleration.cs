using UnityEngine;
using System.Collections;

public class Get_Acceleration : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 acceleration = Input.acceleration;
		Debug.Log (acceleration);
	}
}
