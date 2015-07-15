using UnityEngine;
using System.Collections;
using System.Linq;

public class PakuPaku : MonoBehaviour {

	public AudioClip[] uni_voice;

	//再生中音声の音量取得用
	float[] waveData_ = new float[1024];
	float volume;
	
	//口パク開始フラグ，口パク中，開けるか閉じるか，開き具合
	bool is_paku_start;
	bool is_paku;
	bool is_paku_a;
	float face_a=0.0f;


	public Sprite Normal;
	public Sprite A;
	
	void Start () {
		GetComponent<AudioSource>().Play();
	}
	void Update(){
		
		//再生中音声の音量取得
		AudioListener.GetOutputData(waveData_, 1);
		volume = waveData_.Select(x => x*x).Sum() / waveData_.Length;
		
		//口パク開始
		if (is_paku_start == true) {
			is_paku_start=false;
			
			//開き具合を初期化して口パク開始
			face_a=0.0f;
			is_paku=true;
			StartCoroutine("KutiPaku");
		}



	is_paku_start = true;
	StartCoroutine ("AudioEnd");
}
//音声の終了を検知し，終わったら口パクをやめ
//表情をデフォルトにする
IEnumerator AudioEnd(){
	while (GetComponent<AudioSource>().isPlaying == true) {
		yield return new WaitForSeconds(0.05f);
	}
	is_paku = false;
	SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer> (); 
	renderer.sprite = Normal;
}

//音声が再生中かつ音量が0以上の時口パクをする
//口パクは「あ」の形に開ける→閉じるを繰り返す
IEnumerator KutiPaku(){
	while (is_paku==true) {
		if(volume>0.0f){
			if (is_paku_a==false) {
				face_a = face_a + 0.1f;
				SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer> (); 
				renderer.sprite = A;
				if(face_a>=1.0f){
					is_paku_a=true;
				}
			}else {
				face_a = face_a - 0.1f;
				SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer> (); 
				renderer.sprite = Normal;
				if(face_a<=0.0f){
					is_paku_a=false;
				}
			}
		}else{
			face_a=0.0f;
			SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer> (); 
			renderer.sprite = Normal;
		}
		yield return new WaitForSeconds(0.015f);
	}
}
}