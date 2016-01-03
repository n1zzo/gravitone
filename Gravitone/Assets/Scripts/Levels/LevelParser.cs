﻿using UnityEngine;
using System.Collections;
using SimpleJSON;

public class LevelParser : MonoBehaviour {

	public int levelNumber = 1;
	public GameObject planetKick;
	public GameObject planetSnare;
	public GameObject planetHat;
	public GameObject star;
	public GameObject[] chordPlanet = new GameObject[4];
	public GameObject melody;
	private string toParse;

	// Use this for initialization
	void Start () {
		 LoadLevel();
	}

	void LoadLevel() {
		// Load the JSON file from Resources folder
		TextAsset currentLevel = Resources.Load("Levels/Level"+levelNumber) as TextAsset;
		toParse = currentLevel.text;
		var N = JSON.Parse(toParse);
		// Fill the drum arrays from the JSON file
		for (int i = 0; i < 64; i++) {
			if (N["data"]["kickArray"][i].AsInt == 0)
				planetKick.GetComponent<Drum>().targetDrumArray[i] = false;
			else
				planetKick.GetComponent<Drum>().targetDrumArray[i] = true;
		}
		for (int i = 0; i < 64; i++) {
			if (N["data"]["snareArray"][i].AsInt == 0)
				planetSnare.GetComponent<Drum>().targetDrumArray[i] = false;
			else
				planetSnare.GetComponent<Drum>().targetDrumArray[i] = true;
		}
		for (int i = 0; i < 64; i++) {
			if (N["data"]["hatArray"][i].AsInt == 0)
				planetHat.GetComponent<Drum>().targetDrumArray[i] = false;
			else
				planetHat.GetComponent<Drum>().targetDrumArray[i] = true;
		}
		// Set the star parameters
		star.GetComponent<BeatGen>().bpm = N["data"]["star"]["bpm"].AsInt;
		star.GetComponent<BeatGen>().beatsPerBar = N["data"]["star"]["beatsPerBar"].AsInt;
		star.GetComponent<BeatGen>().subBeatsPerBeat = N["data"]["star"]["subBeatsPerBeat"].AsInt;
		star.GetComponent<BeatGen>().granularity = N["data"]["star"]["granularity"].AsInt;
		// Fill in the chords
		for (int i = 0; i < 4; i++) {
			chordPlanet[i].GetComponent<ChordPlanet>().chordName = N["data"]["chords"][i]["name"];
			chordPlanet[i].GetComponent<ChordPlanet>().baseNote = N["data"]["chords"][i]["note"].AsInt;
			chordPlanet[i].GetComponent<ChordPlanet>().order = i;
		}
		// ...And the melody
		for (int i = 0; i < 7; i++) {
			melody.GetComponent<Melodies>().notes[i] = N["data"]["notes"][i].AsInt;
		}
		for (int i = 0; i < 64; i++) {
			melody.GetComponent<Melodies>().melodyNotes[i] = N["data"]["melody"][i].AsInt;
		}
	}

}