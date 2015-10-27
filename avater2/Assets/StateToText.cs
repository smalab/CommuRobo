using UnityEngine;
using System.Collections;

public class StateToText : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public static void Default(){
		PostureStatus.postureString = "Default";
	}
	public static void  Backten(){
		PostureStatus.postureString = "Backten";
	}
	public static void  Zenten(){
		PostureStatus.postureString = "Zenten";
	}
	public static void Sokuten(){
		PostureStatus.postureString = "Sokuten";
	}
	public static void Sleep(){
		PostureStatus.postureString = "nemui";
	}
}
