using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Text;

public class gyro_GetPosture : MonoBehaviour {


	private int x = 0;
	private int y = 0;
	private int z = 0;
	private string x1;
	private string y1;
	private string z1;
	private string space = " ";
	private float time = 0.0f;


	// Use this for initialization
	void Start () {
		Input.gyro.enabled = true;
		if (File.Exists("Assets/log.csv")) {
			Debug.Log("fileatta");
			FileStream f = new System.IO.FileStream("Assets/log.csv", FileMode.Append, FileAccess.Write);
			Encoding utf8Enc = Encoding.GetEncoding("UTF-8");
			StreamWriter writer = new StreamWriter(f, utf8Enc);
			writer.WriteLine("");
			writer.Close();
		}
	}
	
	// Update is called once per frame
	void Update () {
		//0.2s gotoni data wo toru
		time += Time.deltaTime;
		if (time >= 0.1f) {
			Vector3 accele = Input.acceleration;
			//x = Mathf.RoundToInt (Input.gyro.attitude.x * 10.0f) + 5;
			//y = Mathf.RoundToInt (Input.gyro.attitude.y * 10.0f) - 5;
			//z = Mathf.RoundToInt (Input.gyro.attitude.z * 10.0f) - 5;

			x = Mathf.RoundToInt (accele.x * 10.0f);
			y = Mathf.RoundToInt (accele.y * 10.0f);
			z = Mathf.RoundToInt (accele.z * 10.0f);

			//x = Mathf.RoundToInt (Input.gyro.rotationRate.x * 10);
			//y = Mathf.RoundToInt (Input.gyro.rotationRate.y * 10);
			//z = Mathf.RoundToInt (Input.gyro.rotationRate.z * 10);


			x1 = x.ToString ();
			y1 = y.ToString ();
			z1 = z.ToString ();
			Debug.Log (x1 + " " + y1 + " " + z1);
			logSave (x1, y1, z1, space);
			time = 0.0f;
		}

	}

	public void logSave(string x, string y, string z, string space){

		FileStream f = new FileStream("Assets/log.csv", FileMode.Append, FileAccess.Write);
		Encoding utf8Enc = Encoding.GetEncoding("UTF-8");
		StreamWriter writer = new StreamWriter(f, utf8Enc);
		writer.Write(x + ",");
		writer.Write(y + ",");
		writer.Write(z + ",");
		writer.Write(space + ",");
		writer.Close();

	}
}
