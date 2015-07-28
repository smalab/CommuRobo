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


	IEnumerator AudioEnd(){
		yield return new WaitForSeconds(2);
		
		scene = Random.Range (0, 5);
		Application.LoadLevel(scene);
		Debug.Log ("touch");
		Next_Scene_Positive.score -= 1;
		Debug.Log (Next_Scene_Positive.score);
		
	}


	public void Next_N (){ 
		GetComponent<AudioSource>().Play();
		Debug.Log (55);
		
		StartCoroutine ("AudioEnd");
	}
}