using UnityEngine;
using System.Collections;
using LibPDBinding;

public class OnlineStrings : MonoBehaviour, Strings {

	// The note parameter of the play function represents the target midi
	// note to be played, and will be played instantly after the method
	// is called.

	void Start() {
		// Set puredata output gain
		LibPD.SendFloat("stringsGain", 0.6f);
	}

	void Strings.Play(int note) {
		LibPD.SendFloat("stringsNote", note);
	}

	void Strings.SetVolume(float value) {
		// Set puredata output gain
		LibPD.SendFloat("stringsGain", value);
	}
}
