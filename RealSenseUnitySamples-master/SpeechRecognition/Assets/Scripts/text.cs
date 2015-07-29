using UnityEngine;
using System.Collections;
using RSUnityToolkit;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.UI;

public class text : MonoBehaviour {
	private string oput = "";
	Text textmesh;
	// Use this for initialization
	void Start () {
		textmesh = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
//		if (oput == "" || oput != "") {
//			foreach ( var s in SenseToolkitManager.Instance.SentencesRecognized ) {
//				NyDebug.PushLog( s );
//				oput = (string) s;
//			}
//		}
		textmesh.text = GameObject.Find ("Speech").GetComponent<SpeechRecognition> ().currentText;
	}
}
