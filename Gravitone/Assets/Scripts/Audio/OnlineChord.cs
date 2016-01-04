using UnityEngine;
using System.Collections;
using LibPDBinding;

public class OnlineChord : MonoBehaviour, Chord {

	public GameObject star;

	// Use LibPD.SendFloat("midiNote", 60); to set the fundamental note to C.
	// Then use LibPD.SendBang("M"); to play a major chord.
	// Possible choices are: M, m, M7, m7, 7, dim.

	void Start() {
		// Set puredata output gain
		LibPD.SendFloat("gain", 1f);

		// Set the decays according to the star's BPM
		int bpm = star.GetComponent<BeatGen>().bpm;
		int decay = (2000*bpm)/128;
		// Set chord decays, default value is 2000
		LibPD.SendFloat("ampDecay", decay);
		LibPD.SendFloat("filterDecay", decay);
	}

	void Chord.Play(int note, string type) {
		LibPD.SendFloat("midiNote", note);
		LibPD.SendBang(type);
	}

	void Chord.Stop() {
		LibPD.SendBang("stop");
	}

}
