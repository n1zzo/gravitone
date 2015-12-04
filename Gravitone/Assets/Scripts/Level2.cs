﻿using UnityEngine;
using System.Collections;

public class Level2 : Subscriber {

	public GameObject cam;
	public GameObject star;
	public GameObject wave;
	public GameObject wavePrefab;
	public GameObject prev;
	private int numberOfThirdBeat=0;
	private int currentBar=0;
	public int bars=4;
	public GameObject[] planets;
	private int score;
	public int[] notes = new int[4] {50,50,50,55};
	public string[] types = new string[4] {"M", "m", "M7", "M7"};
	private Vector3[] initialPositions = new Vector3[4];

	// Use this for initialization
	void Start () {

		cam.GetComponent<SmoothCamera>().enabled = true;
		cam.GetComponent<SmoothCamera>().setArrival(10.5f);

		star.GetComponent<BeatGen>().Subscribe(this);

		prev.GetComponent<HarmonyPreview>().setPreview(notes, types);

		wave.SetActive(true);

		numberOfThirdBeat=star.GetComponent<BeatGen>().granularity-star.GetComponent<BeatGen>().subBeatsPerBeat;

		SaveInitialPositions();

	}

	// Update is called once per frame
	void Update () {

	}

	// This method is called for each beat
	public override void Beat(int currentSlot) {

		if(currentSlot==numberOfThirdBeat){
			if (currentBar==bars) {
				Instantiate(wavePrefab, new Vector3(0, 0, 0), Quaternion.identity);
				currentBar=0;
				score=0;
				int placed = 0;
				foreach(GameObject planet in planets)
					if(planet.GetComponent<Drag>().orbitNumber!=-1) {
						placed++;
						if(planet.GetComponent<ChordPlanet>().chordName==types[planet.GetComponent<Drag>().orbitNumber])
							score++;
					}
				if(placed == notes.Length) {
					// The player has placed all the planets check the score
					if(score < notes.Length)
						CollapsePlanets();
					else
						NextLevel();
				}
			}
			currentBar++;
		}



	}

	public void setRadiusPlanets(float[] radius){
		foreach(GameObject planet in planets){
			planet.SetActive(true);
			planet.GetComponent<Drag>().radiusOrbits=radius;
		}
	}

	public void CollapsePlanets() {
		RestorePositions();
		score = 0;
	}

	public void NextLevel() {
		Debug.Log("NEXT LEVEL!");
		score = 0;
	}

	// Saves the initial position of the planets in an array
	private void SaveInitialPositions() {
		int i = 0;
		foreach(GameObject planet in planets) {
			initialPositions[i] = planet.transform.position;
			i++;
		}
	}

	// Reset all the planets to their initial states and positions
	private void RestorePositions() {
		int i = 0;
		foreach(GameObject planet in planets) {
			// Disable the rotation
			planet.GetComponent<Rotate>().enabled=false;
			// Put the planet in its initial position
			planet.transform.position = initialPositions[i];
			// Resets the orbit of the planet
			planet.GetComponent<Drag>().orbitNumber=-1;
			// Makes the planet mute again
			planet.GetComponent<ChordPlanet>().active=false;
			i++;
		}
	}

}
