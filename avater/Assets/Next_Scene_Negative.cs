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
		
		scene = Application.loadedLevel;
		while (scene == Application.loadedLevel) {
			scene = Random.Range (1, 4);
		}
		Application.LoadLevel(scene);
		Debug.Log ("touch");
		Next_Scene_Positive.score -= 1;
		Debug.Log (Next_Scene_Positive.score);
		
	}


	public void Next_N (){ 
		Debug.Log (55);
		
		StartCoroutine ("AudioEnd");
	}

	public void ButtonSound(){
		GetComponent<AudioSource> ().Play ();
	}

}