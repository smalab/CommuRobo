using UnityEngine;
using System.Collections;

public class gyro_gravity : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 acceleration = Input.acceleration;
		Debug.Log (acceleration);
	}
}
