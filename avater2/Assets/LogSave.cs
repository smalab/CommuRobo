/*using UnityEngine;
using System.Collections;
using System.IO;
using System;
public class LogSave : MonoBehaviour{
	public void logSave(int num, string txt){
		StreamWriter sw;
		FileInfo fi;
		fi = new FileInfo(Application.datapath + "FileName.csv");
		sw = fi.AppendText();
		sw.WriteLine("test output");
		sw.Fluse();
		sw.Close();
	}
}*/