using UnityEngine;
using System.Collections;

public class HeadLookTarget : MonoBehaviour {
	public HeadLookController model;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 vec = Input.mousePosition;
		vec.z = 0.4f;
		if(model)model.target = GetComponent<Camera>().ScreenToWorldPoint(vec);

	}
}
