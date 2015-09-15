using UnityEngine;
using System.Collections;

public class gyro_zenkutu : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		Vector3 acceleration = Input.acceleration;
		Debug.Log (acceleration);
		
		
		
		if (acceleration.z >= 0.9) {
			PostureStatus.postureString = "Zenkutu";
		} else {
			PostureStatus.postureString = "Default";
		}
	}
}
