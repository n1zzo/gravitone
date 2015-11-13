using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LibPDBinding;

public class BeatGen : MonoBehaviour {

	List<Subscriber> subscribers = new List<Subscriber>();

	public int bpm = 60;
	public int beatsPerBar = 4;
	public int subBeatsPerBeat = 4;
	public int granularity = 0;
	int lastSlot = 0;
	float timeSpeed = 0f;
	AudioSource sound;

	public float x = 0f;
	public float y = 0f;

	// While progress goes from 0 to 1 we complete one bar
  public float progress = 0f;

	// Use this for initialization
	void Start () {

		// Get the current x and y coordinates
		x = transform.position.x;
		y = transform.position.y;

		// Time s=peed is derived from BPMs
		timeSpeed = (float) bpm / (60 * (float) beatsPerBar);
    granularity = beatsPerBar * subBeatsPerBeat;

		// Loads the AudioSource component
		sound = GetComponent<AudioSource>();
		LibPD.SendBang("highBeat");
  }

	// Each GameObject that calls this is adddded to a list
	public void Subscribe (Subscriber subscriber) {
		subscribers.Add(subscriber);
	}

	// Update is called once per frame
	void Update () {
		int currentSlot = 0;

		// Update the bar's progress
		progress += timeSpeed * Time.deltaTime;

		// Avoid progress overflow
		if (progress >= 1)
			progress -= 1;

		currentSlot = (int)(progress * (float) granularity);

		if(currentSlot!=lastSlot) {
			lastSlot = currentSlot;
      SendBeat(currentSlot);
			if(currentSlot == 0)
				LibPD.SendBang("highBeat");
			else
				if(currentSlot % beatsPerBar == 0)
					LibPD.SendBang("lowBeat");
    }

	}

	void SendBeat(int currentSlot) {
		foreach (Subscriber subscriber in subscribers)
			subscriber.Beat(currentSlot);
	}

}
