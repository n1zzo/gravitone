using UnityEngine;
using System.Collections;

public class OnlineMelody : MonoBehaviour {

	// Loads the chords clip in an on-demand fashion.

	public AudioSource first;
	public AudioSource second;

	public void Play() {
		 if(first.isPlaying){
				second.Play();
			} else {
				first.Play();
			}
	}
}
