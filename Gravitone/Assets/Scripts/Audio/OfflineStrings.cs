using UnityEngine;
using System.Collections;

public class OfflineStrings : MonoBehaviour, Strings {

	private int sampleNote = 60;
	private float mFactor = 1.0594631f;
	public AudioSource sample;

	void Strings.Play(int note) {
		// This changes the audiosource shift to match the target note
		int shift = note - sampleNote;
		float newpitch = Mathf.Pow(mFactor, (float)shift);
		sample.pitch = newpitch;
		sample.Play();
	}
}
