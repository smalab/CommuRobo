﻿using UnityEngine;
using System.Collections;

public class Next_Scene : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

public void Next (){ 
		Application.LoadLevel(0);
		Debug.Log ("touch");
	}
}
