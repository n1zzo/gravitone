using UnityEngine;
using System.Collections;

public class OfflineChord : MonoBehaviour, Chord {

	// Loads the chords clip in an on-demand fashion.

	public AudioSource first;
	public AudioSource second;

	void Chord.Play(int note, string type) {
		if(type=="m")
				type="min";

		 AudioClip noteLoaded = Resources.Load("sounds/SynthChords/" + note.ToString() + type) as AudioClip;

		 if(first.isPlaying){
		 		second.clip=noteLoaded;
				second.Play();
			} else {
				first.clip=noteLoaded;
				first.Play();
			}
	}

}
