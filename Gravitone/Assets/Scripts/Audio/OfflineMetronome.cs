using UnityEngine;
using System.Collections;

public class OfflineMetronome : MonoBehaviour, Metronome {

	public AudioSource highBeat;
	public AudioSource lowBeat;

	void Metronome.HighBeat () {
		highBeat.Play();
	}

	void Metronome.LowBeat () {
		lowBeat.Play();
	}

}
