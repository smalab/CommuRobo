using UnityEngine;
using System.Collections;

public class TextChange : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	public PostureStatus posturestatus;

	// Update is called once per frame
	void Update () {
		this.GetComponent<Text> ().text = posturestatus.Getposture ();
	}
}
