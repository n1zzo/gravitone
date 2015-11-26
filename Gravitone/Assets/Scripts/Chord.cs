using UnityEngine;
using System.Collections;
using LibPDBinding;

public class Chord : MonoBehaviour {

	public string chordName="M";

	public bool active=false;

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
			if(active && other.gameObject.tag=="wave"){
	    // The planet has been hit by a wave and he is in an orbit
			LibPD.SendFloat("midiNote", 57);
			LibPD.SendBang(chordName);
		}
  }

}
