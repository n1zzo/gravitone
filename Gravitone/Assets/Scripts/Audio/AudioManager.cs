using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	public bool online = true;
	public GameObject OnlineMetronome;
	public GameObject OfflineMetronome;
	public GameObject OnlineChord;
	public GameObject OfflineChord;
	Metronome currentMetronome;
	Chord currentChord;

	// Use this for initialization
	void Start () {
		if(online) {
			OnlineMetronome.SetActive(true);
			currentMetronome = OnlineMetronome.GetComponent<OnlineMetronome>();
			currentMetronome.HighBeat();

			OnlineChord.SetActive(true);
			currentChord = OnlineChord.GetComponent<OnlineChord>();
		}
		else {
			OfflineMetronome.SetActive(true);
			currentMetronome = OfflineMetronome.GetComponent<OfflineMetronome>();

			OfflineChord.SetActive(true);
			currentChord = OfflineChord.GetComponent<OfflineChord>();
		}
	}

	public void HighBeat () {
		currentMetronome.HighBeat();
	}

	public void LowBeat () {
		currentMetronome.LowBeat();
	}

	public void PlayChord(int note, string type) {
		currentChord.Play(note, type);
	}

}
