using UnityEngine;
using System.Collections;
using LibPDBinding;

public class Chord : MonoBehaviour {

	// Use LibPD.SendFloat("midiNote", 60); to set the fundamental note to C.
	// Then use LibPD.SendBang("M"); to play a major chord.
	// Possible choices are: M, m, M7, m7, 7, dim.

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D(Collider2D other) {
    // The planet has been hit by a wave
		LibPD.SendFloat("midiNote", 57);
		LibPD.SendBang("m7");
  }

}
