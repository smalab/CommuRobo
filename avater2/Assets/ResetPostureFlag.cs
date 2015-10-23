using UnityEngine;
using System.Collections;

public class ResetPostureFlag : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}



	public static void ResetBackRoll(){
		if (BackRoll.B == 1 && BackRoll.A == 1 && BackRoll.C == 1 && BackRoll.K == 1) {
			BackRoll.B = 0;
			BackRoll.A = 0;
			BackRoll.C = 0;
			BackRoll.K = 0;
			ForwardRoll.F = 0;
			ForwardRoll.O = 0;
			ForwardRoll.R = 0;
			ForwardRoll.Ward = 0;
			CartWheel.flagC = 0;
			CartWheel.flagA = 0;
			CartWheel.flagR = 0;
			CartWheel.flagT = 0;
		}
	}
	public static void ResetForwardRoll(){
		if (ForwardRoll.F == 1 && ForwardRoll.O == 1 && ForwardRoll.R == 1 && ForwardRoll.Ward == 1) {
			BackRoll.B = 0;
			BackRoll.A = 0;
			BackRoll.C = 0;
			BackRoll.K = 0;
			ForwardRoll.F = 0;
			ForwardRoll.O = 0;
			ForwardRoll.R = 0;
			ForwardRoll.Ward = 0;
			CartWheel.flagC = 0;
			CartWheel.flagA = 0;
			CartWheel.flagR = 0;
			CartWheel.flagT = 0;
		}
	}
	public static void ResetCartWheel(){
		if (CartWheel.flagC == 1 && CartWheel.flagA == 1 && CartWheel.flagR == 1 && CartWheel.flagT == 1) {
			BackRoll.B = 0;
			BackRoll.A = 0;
			BackRoll.C = 0;
			BackRoll.K = 0;
			ForwardRoll.F = 0;
			ForwardRoll.O = 0;
			ForwardRoll.R = 0;
			ForwardRoll.Ward = 0;
			CartWheel.flagC = 0;
			CartWheel.flagA = 0;
			CartWheel.flagR = 0;
			CartWheel.flagT = 0;
		}
	}
}
