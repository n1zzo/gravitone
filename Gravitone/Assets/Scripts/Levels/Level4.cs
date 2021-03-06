﻿using UnityEngine;
using System.Collections;

public class Level4 : MonoBehaviour {

	private GameObject audioManager;
	public bool isFreeMode;
	public GameObject ReturnToMM, LevelComplete;

	// Use this for initialization
	void Start () {

		if(isFreeMode)
			audioManager = GetComponent<FreeLevelManager>().audioManager;
		else
			audioManager = GetComponent<LevelManager>().audioManager;

		ShowButtons();

		// Set intruments gain levels
		audioManager.GetComponent<AudioManager>().SetDrumVolume(0.5f);
		audioManager.GetComponent<AudioManager>().SetChordsVolume(0.9f);
		audioManager.GetComponent<AudioManager>().SetStringsVolume(0.3f);
		audioManager.GetComponent<AudioManager>().SetBassVolume(0.5f);

	}

	public void ShowButtons(){

		if(!isFreeMode)
			LevelComplete.SetActive(true);

		ReturnToMM.SetActive(true);

	}

	public void HideButtons(){
		if(!isFreeMode)
			LevelComplete.SetActive(false);

		ReturnToMM.SetActive(false);
	}
}
