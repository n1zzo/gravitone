using UnityEngine;
using System.Collections;
using LibPDBinding;

public class OnlineChord : MonoBehaviour, Chord {

	// Use LibPD.SendFloat("midiNote", 60); to set the fundamental note to C.
	// Then use LibPD.SendBang("M"); to play a major chord.
	// Possible choices are: M, m, M7, m7, 7, dim.

	void Chord.Play(int note, string type) {
		LibPD.SendFloat("midiNote", note);
		LibPD.SendBang(type);
	}

}
