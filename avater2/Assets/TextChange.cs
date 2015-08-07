using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class TextChange : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	public PostureStatus posturestatus;

	// Update is called once per frame
	// postureString ni yori text change
	void Update () {

		this.GetComponent<Text> ().text = PostureStatus.postureString;
	}

}
