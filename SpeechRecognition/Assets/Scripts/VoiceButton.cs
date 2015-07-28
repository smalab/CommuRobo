using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class VoiceButton : MonoBehaviour {

    public UnityEngine.EventSystems.EventTrigger.TriggerEvent voiceButton;

    public string textToHear;

    private SpeechRecognition speechRecognition;
    BaseEventData eventData;

    // Use this for initialization
    void Start () {
        speechRecognition = GameObject.Find("Speech").GetComponent<SpeechRecognition>();
        eventData = new BaseEventData(EventSystem.current);
    }

    // Update is called once per frame
    void Update () {
	    if (speechRecognition.currentText == textToHear)
        {
            eventData.selectedObject = this.gameObject;
            voiceButton.Invoke(eventData);
        }
	}
}
