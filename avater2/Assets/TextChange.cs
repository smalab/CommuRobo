using UnityEngine;
using System.Collections;

public class TextChange : MonoBehaviour {

	PostureStatus posturestatus;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.GetComponent<Text> ().text = posturestatus.Getposture();
	}
}
