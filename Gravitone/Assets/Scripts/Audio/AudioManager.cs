using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	public bool online = true;
	public GameObject OnlineMetronome;
	public GameObject OfflineMetronome;
	public GameObject OnlineChord;
	public GameObject OfflineChord;
	public GameObject OnlineStrings;
	public GameObject OfflineStrings;
	public GameObject onlineButton;
	public GameObject OnlineBass;
	Metronome currentMetronome;
	Chord currentChord;
	Strings currentStrings;
	Strings currentBass;

	// Use this for initialization
	void Start () {
		if(online) {
			OnlineMetronome.SetActive(true);
			currentMetronome = OnlineMetronome.GetComponent<OnlineMetronome>();
			currentMetronome.HighBeat();

			OnlineChord.SetActive(true);
			currentChord = OnlineChord.GetComponent<OnlineChord>();

			OnlineStrings.SetActive(true);
			currentStrings = OnlineStrings.GetComponent<OnlineStrings>();

			//[TODO] Implement offline bass
			currentBass = OnlineBass.GetComponent<OnlineBass>();

			//Set the Button on if the online is selected
			onlineButton.SetActive(online);
		}
		else {
			OfflineMetronome.SetActive(true);
			currentMetronome = OfflineMetronome.GetComponent<OfflineMetronome>();

			OfflineChord.SetActive(true);
			currentChord = OfflineChord.GetComponent<OfflineChord>();

			OfflineStrings.SetActive(true);
			currentStrings = OfflineStrings.GetComponent<OfflineStrings>();
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

	public void StopChord() {
		currentChord.Stop();
	}

	public void PlayStrings(int note) {
		currentStrings.Play(note);
	}

	public void PlayBass(int note) {
		currentBass.Play(note);
	}

	public void setOnline(){
		online=true;
		Start();
	}

	public void setOffline(){
		online=false;
		Start();
	}

}
