﻿using UnityEngine;
using System.Collections;
using LibPDBinding;

public class ChordPlanet : MonoBehaviour {

	public GameObject audioManager;
	public GameObject[] orbits;
	public string chordName="M";
	public bool active=false;
	public int baseNote;
	public int order;


	// Use audioManager.GetComponent<AudioManager>().PlayChord(57, "M");
	// to play a chord, 57 is the target midi note, "M" is the chord type.
	// Possible choices are: M, m, M7, m7, 7, dim.

	void OnTriggerEnter2D(Collider2D other) {
		if(active && other.gameObject.tag=="wave"){

	    // The planet has been hit by a wave and he is in an orbit
			Play();

		}

  }

	public void Play() {
		audioManager.GetComponent<AudioManager>().PlayChord(baseNote, chordName);
	}

	public void Stop() {
		audioManager.GetComponent<AudioManager>().StopChord();
	}

	public void DisablePlanet(){

		// We can adjust this to avoid the CHORD DELAY !!!!
		GetComponent<CircleCollider2D>().radius=1f;

		// Let the planet sound as the wave passes
		GetComponent<ChordPlanet>().active=true;

		// Disable the drag function
		GetComponent<Drag>().enabled=false;
	}

	public void SetOpaqueOrbit(int number) {
		orbits[number].GetComponent<AlphaColor>().SetTransparency(1);
	}

}
