﻿using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;
using LibPDBinding;

public class Menu : MonoBehaviour {

	public GameObject cam;
	public GameObject menu;
	public GameObject menutree;
	public GameObject text;
	public GameObject melodyCanvas;
	public GameObject spaceText;
	private bool isCanvas;
	private bool isSpaceText;
	private GameObject audioManager;

	void Start(){
		audioManager=GameObject.FindWithTag("AudioManager");
	}

	public void Enable() {

		// Turn on the gaussian blur epublic GameObject audioManager;
		if (Application.platform == RuntimePlatform.Android)
			cam.GetComponent<Blur>().enabled = true;
		else
			cam.GetComponent<BlurOptimized>().enabled = true;
		// Enable the menu camera
		menu.SetActive(true);
		// Enable the target objects
		menutree.SetActive(true);

		if(melodyCanvas.activeSelf){
			isCanvas=true;
			melodyCanvas.SetActive(false);
		} else
			isCanvas=false;

			if(spaceText.activeSelf){
				spaceText.SetActive(false);
				isSpaceText=true;
			}

		//Turn off the text on the first phase
		text.SetActive(false);

		audioManager.GetComponent<AudioManager>().StopBass();

		Time.timeScale=0f;

		if(GetComponent<Level4>().enabled)
			GetComponent<Level4>().HideButtons();

	}

	public void Disable() {

		// Turn on the gaussian blur
		if (Application.platform == RuntimePlatform.Android)
			cam.GetComponent<Blur>().enabled = false;
		else
			cam.GetComponent<BlurOptimized>().enabled = false;

		// Disable the menu camera
		menu.SetActive(false);

		// Disable the target objects
		menutree.SetActive(false);

		//Turn on the text on the first phase
		text.SetActive(true);

		if(isCanvas){
			melodyCanvas.SetActive(true);
			isCanvas=false;
		}

		if(isSpaceText){
			spaceText.SetActive(true);
			isSpaceText=false;
		}

		Time.timeScale=1f;

		if(GetComponent<Level4>().enabled)
			GetComponent<Level4>().ShowButtons();

	}

	public void ReturnToMainMenu() {
			Destroy(GameObject.FindWithTag("Globals"));
			Time.timeScale=1f;
			int count=0;
			Application.LoadLevel("menu");
	}
}
