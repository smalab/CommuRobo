using UnityEngine;
using System.Collections;

public class Play_Sounds : MonoBehaviour {
	public AudioClip sound;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void Reply  () {
	GetComponent<AudioSource>().PlayOneShot (sound);
	}
}
