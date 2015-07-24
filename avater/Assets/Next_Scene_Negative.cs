using UnityEngine;
using System.Collections;

public class Next_Scene_Negative : MonoBehaviour {
	private int scene;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void Next_N (){ 
		scene = Random.Range (0, 3);
		Application.LoadLevel(scene);
		Debug.Log ("touch");
		Next_Scene_Positive.score -= 1;
		Debug.Log (Next_Scene_Positive.score);
	}
}