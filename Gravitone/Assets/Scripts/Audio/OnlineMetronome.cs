using UnityEngine;
using System.Collections;
using LibPDBinding;

public class OnlineMetronome : MonoBehaviour, Metronome {

	void Start() {
		// Set puredata output gain
		LibPD.SendFloat("gain", 0.7f);
	}

	void Metronome.HighBeat () {
		LibPD.SendBang("highBeat");
	}

	void Metronome.LowBeat () {
		LibPD.SendBang("lowBeat");
	}

}
