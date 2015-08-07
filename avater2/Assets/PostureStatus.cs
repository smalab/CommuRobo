﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PostureStatus : MonoBehaviour {

    public static string postureString;
	public  static int flag = 0;
	private int scene;
	// Use this for initialization
	void Start () {
		Input.gyro.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (Input.gyro.rotationRateUnbiased.x);
		if (Input.gyro.rotationRateUnbiased.x < -2) {
			postureString = "Please me Wake up";
			flag = 1;
		}

		if (Input.gyro.rotationRateUnbiased.x > 2) {
			postureString = "I'm sleepy";
			flag = 0;
		}


	}
	 public string GetPosture(){
		return postureString;
	}
}
