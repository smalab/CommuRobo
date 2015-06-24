using UnityEngine;
using System.Collections;

public class ChangeFace : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		MMD4MFaceController face = GetComponent<MMD4MFaceController> ();
		if (face) {
			if (Input.GetKeyDown (KeyCode.Z))
				face.SetFace ("smile");
			if (Input.GetKeyDown (KeyCode.X))
				face.SetFace ("normal");
			if (Input.GetKeyDown (KeyCode.C))
				face.SetFace ("shock");
			if (Input.GetKeyDown (KeyCode.V))
				face.SetFace ("angry");
			if (Input.GetKeyDown (KeyCode.Space))
				face.SetFace ("megane");
		}
	}
}
