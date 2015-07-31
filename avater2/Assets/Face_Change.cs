using UnityEngine;
using System.Collections;

public class Face_Change : MonoBehaviour {
	public Sprite Normal;
	public Sprite Normal_2;
	public Sprite A;
	public Sprite I;
	public Sprite U;
	public Sprite E;
	public Sprite O;

	public Sprite Smile;
	public Sprite Angry;
	public Sprite Sad;
	public Sprite Excite;

	public Sprite Defalt0;
	public Sprite Defalt1;
	public Sprite Defalt2;
	public Sprite Defalt3;
	public Sprite Defalt4;
	public Sprite Defalt5;
	public Sprite Defalt6;
	public Sprite Defalt7;
	public Sprite Defalt8;
	public Sprite Defalt9;
	public Sprite Defalt10;
	public Sprite Defalt11;
	public Sprite Defalt12;
	public Sprite Defalt13;
	public Sprite Defalt14;


	//private float time;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	/*	if (Input.GetKeyDown (KeyCode.Space)) {
			SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer> (); 
			renderer.sprite = Normal;
		}
		if (Input.GetKeyDown (KeyCode.N)) {
			SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer> (); 
			renderer.sprite = Normal_2;
		}
		if (Input.GetKeyDown (KeyCode.A)) {
			SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer> (); 
			renderer.sprite = A;
		}
		if (Input.GetKeyDown (KeyCode.I)) {
			SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer> (); 
			renderer.sprite = I;
		}
		if (Input.GetKeyDown (KeyCode.U)) {
			SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer> (); 
			renderer.sprite = U;
		}
		if (Input.GetKeyDown (KeyCode.E)) {
			SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer> (); 
			renderer.sprite = E;
		}
		if (Input.GetKeyDown (KeyCode.O)) {
			SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer> (); 
			renderer.sprite = O;
		}

		if (Input.GetKeyDown (KeyCode.Z)) {
			SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer> (); 
			renderer.sprite = Smile;
		}
		if (Input.GetKeyDown (KeyCode.X)) {
			SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer> (); 
			renderer.sprite = Angry;
		}
		if (Input.GetKeyDown (KeyCode.C)) {
			SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer> (); 
			renderer.sprite = Sad;
		}
		if (Input.GetKeyDown (KeyCode.V)) {
			SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer> (); 
			renderer.sprite = Excite;
		}                                                                              */






		//scoreによって変化

		//悲しみ
		if (Next_Scene_Positive.score  <= -4) {
			SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer> (); 
			renderer.sprite = Defalt8;
		}
		if (Next_Scene_Positive.score == -3) {
			SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer> (); 
			renderer.sprite = Defalt9;
		}
		if (Next_Scene_Positive.score == -2) {
			SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer> (); 
			renderer.sprite = Defalt7;
		}
		if (Next_Scene_Positive.score == -1) {
			SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer> (); 
			renderer.sprite = Defalt6;
		}


		//基準の表情
		if (Next_Scene_Positive.score == 0) {
			SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer> (); 
			renderer.sprite = Defalt0;
		}

		//喜び
		if (Next_Scene_Positive.score == 1) {
			SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer> (); 
			renderer.sprite = Defalt11;
		}

		if (Next_Scene_Positive.score == 2) {
			SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer> (); 
			renderer.sprite = Defalt1;
		}

		if (Next_Scene_Positive.score >= 3) {
			SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer> (); 
			renderer.sprite = Defalt3;
		}
		if (Next_Scene_Positive.score >= 4) {
			SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer> (); 
			renderer.sprite = Defalt2;
		}


	}
}
