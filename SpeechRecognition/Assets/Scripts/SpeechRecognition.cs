﻿using UnityEngine;
using System.Collections;
using RSUnityToolkit;
using System.Linq;
using System.Collections.Generic;

public class SpeechRecognition : MonoBehaviour {

    //TestChat _testChat; // for chat

    public string currentText = "";
    // Use this for initialization

    void Start () {
        var senseManager = GameObject.FindObjectOfType( typeof( SenseToolkitManager ) );
        if ( senseManager == null ) {
            Debug.LogWarning( "Sense Manager Object not found and was added automatically" );
            senseManager = (GameObject)Instantiate( Resources.Load( "SenseManager" ) );
            senseManager.name = "SenseManager";
        }

        SenseToolkitManager.Instance.SetSenseOption( SenseOption.SenseOptionID.Speech );

        SenseToolkitManager.Instance.AddSpeechCommand( "赤" );
        SenseToolkitManager.Instance.AddSpeechCommand("テスト");

        //_testChat = GameObject.Find("PhotonChat").GetComponent<TestChat>();  //for chat
    }

    // Update is called once per frame
    void Update() {
        if ( (SenseToolkitManager.Instance.SpeechOutput != null) && (SenseToolkitManager.Instance.SpeechOutput.Count != 0) ) {
            Debug.Log( SenseToolkitManager.Instance.SpeechOutput.First().Key );
        }

        if ( (SenseToolkitManager.Instance.SentencesRecognized != null) && (SenseToolkitManager.Instance.SentencesRecognized.Count != 0) ) {
            foreach (var s in SenseToolkitManager.Instance.SentencesRecognized ) {
                // _testChat.TextInput(s); // for chat
                currentText = s;
                Debug.Log(s);
            }
        }
    }
}
