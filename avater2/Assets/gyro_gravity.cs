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

		if (acceleration.y <= -0.9) {
			PostureStatus.postureString = "normal";
		}
		if (acceleration.y >= 0.9) {
			PostureStatus.postureString = "sakadathi";
		}
	}
}
