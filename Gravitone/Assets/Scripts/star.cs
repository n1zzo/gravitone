﻿using UnityEngine;
using System.Collections;

public class star : MonoBehaviour {

	public float x = 0f;
	public float y = 0f;
	public int bpm = 60;
	public int beatsPerBar = 4;
	float scaleStep=0.03f;
	float timeSpeed = 0f;
	int lastBeat=0;

	// While progress goes from 0 to 1 we complete one bar
  public float progress = 0f;

	// Use this for initialization
	void Start () {
		x = transform.position.x;
		y = transform.position.y;

		// Time speed is derived from BPMs
		timeSpeed = (float) bpm / (60 * (float) beatsPerBar);
	}

	// Update is called once per frame
	void Update () {
		// Update the bar's progress
		progress += timeSpeed * Time.deltaTime;

		float scaleFactor = scaleStep * Time.deltaTime;
		transform.localScale -= new Vector3(scaleFactor, scaleFactor, 0);

		// Avoid progress overflow
		if (progress >= 1)
			progress -= 1;

		int currentBeat=checkBeat(progress);

		if(currentBeat!=lastBeat){
			transform.localScale = new Vector3(0.25f, 0.25f, 1);
			lastBeat=currentBeat;
		}
	}

	// Checks if the current angle corresponds to a beat within a given error range
 	int checkBeat (float progress) {
		return (int) (progress * (float) beatsPerBar);
	}
}