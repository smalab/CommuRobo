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

	//private float time;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Space)) {
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
		}

	/*	time += Time.deltaTime; 
		Debug.Log(time);
		if ((int)time % 7 == 3) {
			SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer> (); 
			renderer.sprite = Normal_2;
		}
		else if ((int)time % 7 == 4) {
			SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer> (); 
			renderer.sprite = Normal;
		}                                                                           */

	}
}
