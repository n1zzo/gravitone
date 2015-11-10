using UnityEngine;
using System.Collections;

public class Widen : Subscriber {

	public GameObject star;
	float scaleStep=0.5f;
	int currentSlot = 0;
	int lastSlot = -1;
	bool lastBeat = false;
	int beatsPerBar = 4;

	// Use this for initialization
	void Start () {
		star.GetComponent<BeatGen>().Subscribe(this);
		beatsPerBar = star.GetComponent<BeatGen>().beatsPerBar;
	}

	// Update is called once per frame
	void Update () {

		if(transform.localScale.x>0.75){
			float scaleFactor = scaleStep * Time.deltaTime;
			transform.localScale -= new Vector3(scaleFactor, scaleFactor, 0);
		}

		// Plays the sound if the current slot is full, only one time
    if (lastBeat && currentSlot % beatsPerBar == 0) {
			transform.localScale = new Vector3(1, 1, 1);
      lastSlot=currentSlot;
    }

		lastBeat = false;
	}

	// This method is called for each beat
	public override void Beat(int currentSlot) {
		this.currentSlot = currentSlot;
		lastBeat = true;
	}

}
