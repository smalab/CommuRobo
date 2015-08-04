using UnityEngine;
using System.Collections;

public class gyro : MonoBehaviour {


	void Start () {
		Input.gyro.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (Input.gyro.rotationRateUnbiased.x);
		

	}
}
