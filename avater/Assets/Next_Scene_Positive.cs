using UnityEngine;
using System.Collections;

public class Next_Scene_Positive : MonoBehaviour {

	private int scene;
	public static int score = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

public void Next_P (){ 
		scene = Random.Range (0, 4);
		Application.LoadLevel(scene);
		Debug.Log ("touch");
		score += 1;
		Debug.Log (score);
	}
}
