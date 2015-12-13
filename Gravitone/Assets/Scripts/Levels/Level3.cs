﻿using UnityEngine;
using System.Collections;

public class Level3 : MonoBehaviour {

	public GameObject cam;
	public GameObject[] planets;
	public GameObject background;
	private GameObject audioManager;
	public GameObject melody;
	public GameObject canvas;


	// Use this for initialization
	void Start () {
		melody.SetActive(true);
		canvas.SetActive(true);
		background.GetComponent<Fade>().enabled=true;
		background.GetComponent<Fade>().final=0.9f;
		background.GetComponent<SpriteRenderer>().enabled=true;

		foreach(GameObject planet in planets){
			planet.GetComponent<SpriteRenderer>().enabled=false;
			planet.GetComponent<Rotate>().enabled=false;
			planet.GetComponent<Drag>().enabled=false;
		}

		// Set the camera and menu to follow the first planet.
		cam.GetComponent<SmoothFollow2D>().target = planets[0].transform;
		cam.GetComponent<SmoothFollow2D>().enabled = true;
		cam.GetComponent<SmoothCamera>().setArrival(6f);
		cam.GetComponent<SmoothCamera>().enabled=true;

		planets[0].GetComponent<SpriteRenderer>().enabled=true;

		melody.GetComponent<Melodies>().planets=planets;

		audioManager = GetComponent<LevelManager>().audioManager;
		//audioManager.GetComponent<AudioManager>().PlayStrings(60);

	}

	// Update is called once per frame
	void Update () {

	}

	public void Restart(){
		melody.GetComponent<Melodies>().calculateTotalNotes();
	}
}
