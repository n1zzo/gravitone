using UnityEngine;
using System.Collections;
using LibPDBinding;

public class OnlineBass : MonoBehaviour, Bass {

	// The note parameter of the play function represents the target midi
	// note to be played, and will be played instantly after the method
	// is called.

	void Start() {
		// Set puredata output gain
		LibPD.SendFloat("bassGain", 0.6f);
	}

	void Bass.Play(int note) {
		LibPD.SendFloat("bassNote", note);
	}

	void Bass.Stop() {
		LibPD.SendBang("bassStop");
	}

	void Bass.SetDecay(int decay) {
		LibPD.SendFloat("bassDecay", decay);
	}

	void Bass.SetVolume(float value) {
		// Set puredata output gain
		LibPD.SendFloat("bassGain", value);
	}

}
