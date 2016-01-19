using UnityEngine;
using System.Collections;

public class Level4 : MonoBehaviour {

	private GameObject audioManager;

	// Use this for initialization
	void Start () {

		audioManager = GetComponent<FreeLevelManager>().audioManager;

		// Set intruments gain levels
		audioManager.GetComponent<AudioManager>().SetDrumVolume(0.6f);
		audioManager.GetComponent<AudioManager>().SetChordsVolume(0.8f);
		audioManager.GetComponent<AudioManager>().SetStringsVolume(0.4f);
		audioManager.GetComponent<AudioManager>().SetBassVolume(0.6f);

	}

	// Update is called once per frame
	void Update () {

	}
}
