using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BeatGen : MonoBehaviour {

	List<Subscriber> subscribers = new List<Subscriber>();

	public int bpm = 60;
	public int beatsPerBar;
	public int subBeatsPerBeat;
	public int granularity = 0;
	public int lastSlot = 0;
  float timeSpeed = 0f;

	// While progress goes from 0 to 1 we complete one bar
  public float progress = 0f;

	// Use this for initialization
	void Start () {

    CalculateGranularity();

  }

	public void CalculateGranularity(){
		// Time s=peed is derived from BPMs
		timeSpeed = ((float)bpm * 4) / (60 * (float)beatsPerBar * (float) subBeatsPerBeat);

		granularity=subBeatsPerBeat*beatsPerBar;
	}

	// Each GameObject that calls this is adddded to a list
	public void Subscribe (Subscriber subscriber) {
		subscribers.Add(subscriber);
	}

	// Each GameObject that calls this is adddded to a list
	public void Unsubscribe (Subscriber subscriber) {
		subscribers.Remove(subscriber);
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
    }

	}

	void SendBeat(int currentSlot) {
		foreach (Subscriber subscriber in subscribers.ToArray())
			subscriber.Beat(currentSlot);
	}

}
