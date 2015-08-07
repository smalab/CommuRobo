using UnityEngine;
using System.Collections;
using System.Linq;

public class acceleration : MonoBehaviour {

	// Use this for initialization
	void Start () {
	Input.gyro.enabled = true;
	}
	private int flagx = 0;
	private int flagy = 0;

	private int flagxx = 0;
	private int flagyy = 0;


	// Update is called once per frame
	// kasokudo no syori
	void Update () {
		Debug.Log (Input.acceleration.x);
		Debug.Log (Input.acceleration.y);

		if (Input.acceleration.x < -0.5)
			flagx = 1;

		if (Input.acceleration.y < -0.5)
			flagy = 1;

		if (Input.acceleration.x > 0.5)
			flagxx = 1;
		
		if (Input.acceleration.y >-0.5)
			flagyy = 1;

		if (flagx == 1 && flagxx == 1 && flagy == 1 && flagyy == 1)
			StartCoroutine("Accele");

	}

	IEnumerator Accele(){
		yield return new WaitForSeconds (2);

		PostureStatus.postureString = "Don't Shake !";
	}
}
