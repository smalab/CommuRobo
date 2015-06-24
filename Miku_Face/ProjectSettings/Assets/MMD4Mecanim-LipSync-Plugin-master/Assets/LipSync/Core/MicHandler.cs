using UnityEngine;
using System.Collections;

[RequireComponent (typeof (AudioSource))]
public class MicHandler : MonoBehaviour 
{
	private int  sampleCount_ = 1024;
	private int  minFreq_;
	private int  maxFreq_;
	private bool initialized_ = false;
	private bool recording_   = false;
	private float[] data_;
	private int lastFrameCount_ = -1;

	public bool isReady {
		get { return initialized_; }
	}

	public bool isRecording {
		get { return recording_; }
	}

	public float df {
		get { return GetComponent<AudioSource>().clip ?
			GetComponent<AudioSource>().clip.frequency / sampleCount_ : 0.0f; }
	}

	public AudioClip clip {
		get { return GetComponent<AudioSource>().clip; }
	}

	void Update()
	{
		if (!GetComponent<AudioSource>().isPlaying && initialized_ && recording_) {
			GetComponent<AudioSource>().clip = Microphone.Start(null, false, 10, maxFreq_);
			GetComponent<AudioSource>().mute = true;
			while (Microphone.GetPosition(null) <= 0) {}
			GetComponent<AudioSource>().Play();
		}
	}

	void OnApplicationPause()
	{
		GetComponent<AudioSource>().Stop();
		Destroy(GetComponent<AudioSource>().clip);
	}

	public void Initialize(int sampleCount = 1024)
	{
		sampleCount_ = sampleCount;
		data_ = new float[sampleCount];

		// Check if microphone exists
		if (Microphone.devices.Length <= 0) {
			Debug.LogWarning("Microphone not connected!");
			return;
		} else {
			Debug.Log("Use:" + Microphone.devices[0]);
		}

		// Get default microphone min/max frequencies
		Microphone.GetDeviceCaps(null, out minFreq_, out maxFreq_);
		if (minFreq_ == 0 && maxFreq_ == 0) {
			maxFreq_ = 44100;
		} else if (maxFreq_ > 44100) {
			maxFreq_ = 44100;
		}
		initialized_ = true;
	}

	public void Record()
	{
		if (!initialized_) {
			Debug.LogError("Mic has not been initialized yet!");
		} else {
			recording_ = true;
		}
	}

	public void Stop()
	{
		GetComponent<AudioSource>().Stop();
		Destroy(GetComponent<AudioSource>().clip);
		recording_ = false;
	}

	public float[] GetData()
	{
		if (lastFrameCount_ != Time.frameCount) {
			lastFrameCount_ = Time.frameCount;
			GetComponent<AudioSource>().GetOutputData(data_, 0);
		}
		return data_;
	}
}
