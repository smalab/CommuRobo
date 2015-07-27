using UnityEngine;
using System.Collections;

public class Next_Scene_Positive : MonoBehaviour {

	private int scene;
	public static int score = 0;
	public double i =0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	//onsei saiseigo scene no henkou
public void Next_P (){ 
		GetComponent<AudioSource>().Play();
		while(i<100000000){

			i += 0.1;
		}

		scene = Random.Range (0, 5);
		Application.LoadLevel(scene);
		Debug.Log ("touch");
		score += 1;
		Debug.Log (score);
	}
}
