using UnityEngine;
using System.Collections;

public class gyrocontroller : MonoBehaviour {
	private string gyro;
	// Use this for initialization
	void Start () {

	}

public static void Ongyro(){
		Input.gyro.enabled = true;
	}

public static void Offgyro(){
		Input.gyro.enabled = false;
	}

	public static float y_gyro(){
		return Input.gyro.rotationRateUnbiased.y;
	}

		// Update is called once per frame
	void Update () {

	}
}
