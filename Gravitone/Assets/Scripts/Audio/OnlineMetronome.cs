using UnityEngine;
using System.Collections;
using LibPDBinding;

public class OnlineMetronome : MonoBehaviour, Metronome {

	void Metronome.HighBeat () {
		LibPD.SendBang("highBeat");
	}

	void Metronome.LowBeat () {
		LibPD.SendBang("lowBeat");
	}

}
