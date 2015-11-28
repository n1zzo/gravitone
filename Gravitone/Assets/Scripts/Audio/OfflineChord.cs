using UnityEngine;
using System.Collections;

public class OfflineChord : MonoBehaviour, Chord {

	// Loads the chords clip in an on-demand fashion.

	public AudioSource first;
	public AudioSource second;

	void Chord.Play(int note, string type) {

	}

}
