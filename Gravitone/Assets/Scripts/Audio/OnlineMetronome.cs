using UnityEngine;
using System.Collections;
using LibPDBinding;

public class OnlineMetronome : MonoBehaviour, Metronome {

	void Start() {
		// Set puredata output gain
		LibPD.SendFloat("gain", 1f);
	}

	void Metronome.HighBeat () {
		LibPD.SendBang("highBeat");
	}

	void Metronome.LowBeat () {
		LibPD.SendBang("lowBeat");
	}

}
