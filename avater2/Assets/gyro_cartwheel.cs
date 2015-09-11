using UnityEngine;
using System.Collections;

public class gyro_cartwheel : MonoBehaviour {

	private bool tate = false;
	private bool yoko = false;
	private bool reverse = false;


	// Use this for initialization
	void Start () {
	
	}




	// Update is called once per frame
	void Update () {
		Vector3 acceleration = Input.acceleration;
		Debug.Log (acceleration);
		
		if (acceleration.x <= 0.0 && acceleration.y <= -0.9) 
			tate = true;

		if (acceleration.x >= 0.4 && acceleration.x <= 0.9 && acceleration.y >= -0.7 && acceleration.y <= 0.2)
			yoko = true;

		if (acceleration.x >= -0.2 && acceleration.x <= 0.2 && acceleration.y >= 0.8 && acceleration.y <= 1.1) {
			tate = false;
			reverse = true;
		}



		if(tate == true && yoko == true && reverse == true){
			PostureStatus.postureString = "Sokuten";
		}

	}

}
