using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Queue : MonoBehaviour {

	private BackRoll backroll;

	// Use this for initialization
	void Start () {
		Queue<string> queue = new Queue<string>(){};
		queue.Enqueue("kongo");
		queue.Enqueue("hiei");
		queue.Enqueue("haruna");
		queue.Enqueue("kirishima");


		Debug.Log("queue.Count:" + queue.Count);
		
		foreach (string name in queue) {
			Debug.Log("name:" + name);
		}
		
		while (queue.Count > 0) {
			Debug.Log("queue.Dequeue():" + queue.Dequeue());
		}
		
		Debug.Log("queue.Count:" + queue.Count);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void StateQueue(){
		Queue<string> queue = new Queue<string>(5){};
		//backroll = Get_Acceleration.anim.GetBehaviour<BackRoll>;

		queue.Enqueue ("BackRoll.stateInfo1.IsName");

	}
}
