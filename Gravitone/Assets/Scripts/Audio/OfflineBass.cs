using UnityEngine;
using System.Collections;

public class OfflineBass : MonoBehaviour, Bass {

	private int sampleNote = 60;
	private float mFactor = 1.0594631f;
	public AudioSource sample;

	void Bass.Play(int note) {
		// This changes the audiosource shift to match the target note
		int shift = note - sampleNote;
		float newpitch = Mathf.Pow(mFactor, (float)shift);
		sample.pitch = newpitch;
		sample.Play();
	}

    void Bass.Stop() {
        sample.Stop();
    }

    void Bass.SetDecay(int decay) {
        // Lol this does not exist in the offline version!
        ;
    }

	void Bass.SetVolume(float value) {
		sample.volume = value;
	}
}
