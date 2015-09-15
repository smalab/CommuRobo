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
		if (PostureStatus.postureString == "Default") {
			this.GetComponent<Text> ().text = "as usual";
		}



		
		if (PostureStatus.postureString == "nemui") {
			//this.GetComponent<Text> ().text = PostureStatus.postureString;
			this.GetComponent<Text> ().text = "I'm Sleepy";
		}

		if (PostureStatus.postureString == "Please me Wake up") {
			//this.GetComponent<Text> ().text = PostureStatus.postureString;
			this.GetComponent<Text> ().text = "Wake Up !!!";
		}

		if (PostureStatus.postureString == "Shake now") {
			//this.GetComponent<Text> ().text = PostureStatus.postureString;
			this.GetComponent<Text> ().text = "Don't Shake !!!!!";
		}



		if (PostureStatus.postureString == "normal") {
			this.GetComponent<Text> ().text = "Let's sakadathi !";
		}

		if (PostureStatus.postureString == "sakadathi") {
			this.GetComponent<Text> ().text = "mou yametekure";
		}



		if (PostureStatus.postureString == "Up") {
			this.GetComponent<Text> ().text = "agatte masu";
		}
		
		if (PostureStatus.postureString == "Down") {
			this.GetComponent<Text> ().text = "sagatte masu";
		}
		if (PostureStatus.postureString == "Ayasu") {
			this.GetComponent<Text> ().text = "Ayasarete imasu";
		}


		if (PostureStatus.postureString == "Sokuten") {
			this.GetComponent<Text> ().text = "Sokuten site masu";
		}

		if (PostureStatus.postureString == "Zenkutu") {
			this.GetComponent<Text> ().text = "Zenkutu site masu";
		}



	}

}
