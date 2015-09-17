using UnityEngine;
using System.Collections;

public class gyro_BackRoll : MonoBehaviour {

	private bool tate = false;
	private bool yoko = false;
	private bool reverse = false;
	
	
	// Update is called once per frame
	// kasokudo de sokuten
	void Update () {
		Vector3 acceleration = Input.acceleration;
		Debug.Log (acceleration);
		
		
		if (acceleration.x <= 0.2 && acceleration.y <= -0.9) 
			tate = true;
		
		if (acceleration.z <= -1.0)
			yoko = false;
		
		if (acceleration.x >= -0.2 && acceleration.x <= 0.2 && acceleration.y >= 0.8 && acceleration.y <= 1.1  && acceleration.z <= 0.2) {
			tate = false;
			reverse = true;
		}
		
		if (acceleration.z >= 1.0)
			yoko = true;
		
		if(tate == true && yoko == true && reverse == true){
			PostureStatus.postureString = "Backten";
		}
		
	}
	
}