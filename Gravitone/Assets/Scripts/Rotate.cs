﻿using UnityEngine;
using System.Collections;

public class Rotate : Subscriber {

	public GameObject star;
	public float radius = 1f;
	public bool clockwise = true;
	float starX;
	float starY;
	int currentBar=0;
	int bars=4;
	const float TWO_PI = 2*Mathf.PI;

	// Use this for initialization
	void Start () {

		// Gets the x and y coordinates and bpm from the reference star
		starX = star.GetComponent<BeatGen>().x;
		starY = star.GetComponent<BeatGen>().y;

		star.GetComponent<BeatGen>().Subscribe(this);

		// Sets the initial position
		transform.position = new Vector3(0, radius, 0);

	}

	// Update is called once per frame
	void Update () {


		// Gets the current progress from the star
		float progress = star.GetComponent<BeatGen>().progress;

		progress=(progress + currentBar)/bars;

		// Calculate the planet's position
		transform.position = getPosition(progress * TWO_PI);

	}

	// Obtains the planet position from the planet's angle
	Vector3 getPosition (float angle) {
		if(clockwise)
			return new Vector3(radius*Mathf.Sin(angle) + starX, radius*Mathf.Cos(angle) + starY, 0);
		else
			return new Vector3(radius*Mathf.Sin(-angle) + starX, radius*Mathf.Cos(-angle) + starY, 0);
	}

	// This method is called for each beat
	public override void Beat(int currentSlot) {
			if(currentSlot==0){
				if(currentBar<bars-1)
					currentBar++;
				else
					currentBar=0;
		 	}
	}

	public void setRadius (float rad){
		radius=rad;
	}

}
