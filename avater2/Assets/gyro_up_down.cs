using UnityEngine;
using System.Collections;

public class gyro_up_down : MonoBehaviour {
	private int flag = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 acceleration = Input.acceleration;
		Vector3 rotation = Input.gyro.rotationRateUnbiased;
		Debug.Log (acceleration);
		Debug.Log(Input.gyro.rotationRateUnbiased.y);

		if (acceleration.y <= -1.8) {
			PostureStatus.postureString = "Down";
			flag = 0;
		}
		if (acceleration.y >= -0.5) {
			PostureStatus.postureString = "Up";
			flag = 1;
		}
		
		
		//flag ga 1 no toki yoko ni huruto yosiyosi tekinamononi

		if (flag == 1 && Input.gyro.rotationRateUnbiased.y > 1.8) {
			PostureStatus.postureString = "Ayasu";


		}
	}
}
