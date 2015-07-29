using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class VoiceButton : MonoBehaviour {

    public UnityEngine.EventSystems.EventTrigger.TriggerEvent voiceButton;

    public string textToHear;
	string[] textsToHear;
    private SpeechRecognition speechRecognition;
    BaseEventData eventData;

    // Use this for initialization
    void Start () {
        speechRecognition = GameObject.Find("Speech").GetComponent<SpeechRecognition>();
        eventData = new BaseEventData(EventSystem.current);
		textsToHear = textToHear.Split (';');
    }

    // Update is called once per frame
    void Update () {
		foreach (string s in textsToHear) {
			if (speechRecognition.currentText.Contains(s)) {
				speechRecognition.currentText = "";
				voiceButton.Invoke (eventData);
			}
		}
	}
}
