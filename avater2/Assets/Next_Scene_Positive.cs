using UnityEngine;
using System.Collections;
using System.Linq;

public class Next_Scene_Positive : MonoBehaviour {

	private int scene;
	public static int score = 0;
	public double i =0;



	float[] waveData_ = new float[1024];
	float volume;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		//再生中音声の音量取得
		AudioListener.GetOutputData(waveData_, 1);
		volume = waveData_.Select(x => x*x).Sum() / waveData_.Length;


	}


	IEnumerator AudioEnd(){
			yield return new WaitForSeconds(2);

		scene = Application.loadedLevel;
		while (scene == Application.loadedLevel) {
			scene = Random.Range (1, 6);
		}
		Application.LoadLevel(scene);
		Debug.Log ("touch");
		score += 1;
		Debug.Log (score);

		}



	//onsei saiseigo scene no henkou
	public void Next_P (){ 
		Debug.Log (55);

		StartCoroutine ("AudioEnd");
	}

	public void ButtonSound(){
		GetComponent<AudioSource> ().Play ();
	}
}
