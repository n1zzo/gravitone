using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class beatGen : MonoBehaviour {

	List<GameObject> subscribers = new List<GameObject>();

	public int bpm = 60;
	public int beatsPerBar = 4;
	public int subBeatsPerBeat = 4;
	int currentSlot = 0;
	int lastSlot=-1;
	float timeSpeed = 0f;
	int granularity = 0;

	// While progress goes from 0 to 1 we complete one bar
  float progress = 0f;

	// Use this for initialization
	void Start () {
		// Time s=peed is derived from BPMs
		timeSpeed = (float) bpm / (60 * (float) beatsPerBar);
    granularity = beatsPerBar * subBeatsPerBeat;
  }

	// Each GameObject that calls this is adddded to a list
	void Subscribe (GameObject subscriber) {
		subscribers.Add(subscriber);
	}

	// Update is called once per frame
	void Update () {

		// Update the bar's progress
		progress += timeSpeed * Time.deltaTime;

		// Avoid progress overflow
		if (progress >= 1)
			progress -= 1;

		currentSlot = (int)(progress * (float) granularity);

		if(currentSlot!=lastSlot) {
			currentSlot=lastSlot;
      Beat();
    }

	}

	void Beat() {
		// il Da Farsi
		foreach (GameObject subscriber in subscribers)
					subscriber.playSlot(currentSlot);
	}

}
