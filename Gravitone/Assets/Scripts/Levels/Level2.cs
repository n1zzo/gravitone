﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Level2 : Subscriber {

	public GameObject cam;
	public GameObject menucam;
	public GameObject star;
	public GameObject wave;
	public GameObject wavePrefab;
	public GameObject prev;
    public GameObject textField;
	public bool autocomplete=false;
	public int numberOfThirdBeat=0;
	public int currentBar=0;
	public int bars=4;
	public GameObject[] planets;
	private int score;
	private int placed;
	public int[] notes;
	public string[] types;
	int restoreCount=0;
	bool isWaiting=false;
	bool isPreview=false;
	GameObject actualWave;
	GameObject audioManager;
	bool[] assigned = new bool[4];


	// Use this for initialization
	void Start () {
        // Set GUI text field
        textField.GetComponent<Text>().text = "Listen to the chords";

		audioManager = GetComponent<LevelManager>().audioManager;

		audioManager.GetComponent<AudioManager>().GetDrums();

		// Set intruments gain levels
		audioManager.GetComponent<AudioManager>().SetDrumVolume(0.3f);
		audioManager.GetComponent<AudioManager>().SetChordsVolume(1f);
		//audioManager.GetComponent<AudioManager>().SetStringsVolume(0.6f);
		//audioManager.GetComponent<AudioManager>().SetBassVolume(0.6f);

		// Adjust the main cam and menu cam to the second level position
		cam.GetComponent<SmoothCamera>().enabled = true;
		cam.GetComponent<SmoothCamera>().setArrival(10.5f);
		menucam.GetComponent<SmoothCamera>().enabled = true;
		menucam.GetComponent<SmoothCamera>().setArrival(10.5f);

		star.GetComponent<BeatGen>().Subscribe(this);

		prev.GetComponent<HarmonyPreview>().setPreview(notes, types);

		wave.SetActive(true);

		numberOfThirdBeat=star.GetComponent<BeatGen>().granularity-(star.GetComponent<BeatGen>().subBeatsPerBeat);

	}

	// Update is called once per frame
	void Update () {

	}

	// This method is called for each beat
	public override void Beat(int currentSlot) {

		if(currentSlot==numberOfThirdBeat){
			if (currentBar==bars) {

				if(isWaiting) {

					// The player has placed all the planets check the score
					if(score < notes.Length){
						CollapsePlanets();
						isPreview=true;
						InstantiateWave();
					}
					else
						NextLevel();

					isWaiting=false;

				} else
					InstantiateWave();

				currentBar=0;

			}
			currentBar++;
		}
	}

	void InstantiateWave(){
		if(!isPreview)
			actualWave=Instantiate(wavePrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
		else {
				isPreview=false;
				wave.SetActive(true);
				wave.GetComponent<Wave>().Restart();
				isWaiting=false;
		}
	}

	public void CheckCorrectness(){

        textField.GetComponent<Text>().text = "";

		placed ++;

		if(placed == notes.Length){

			score=0;
			foreach(GameObject planet in planets)
				if(planet.GetComponent<Drag>().orbitNumber!=-1)
					if(CheckPlanet(planet))
						score++;

			isWaiting=true;
			DisablePlanets();
			GetComponent<LevelManager>().SetGreyBackground();

			//Stop to avoid Overlap
			planets[0].GetComponent<ChordPlanet>().Stop();

			// begin the preview from the beginning
			currentBar=0;
			Destroy(actualWave);
			actualWave=Instantiate(wavePrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
			actualWave.transform.localScale=new Vector3(0.186f, 0.186f, 1);
			star.GetComponent<BeatGen>().progress=0f;

			//first planet is missing
			GetFirstOrbitPlanet().GetComponent<ChordPlanet>().Play();
		}
		else
			isWaiting=false;

	}

	bool CheckPlanet(GameObject planet){
		int num=planet.GetComponent<Drag>().orbitNumber;

		return planet.GetComponent<ChordPlanet>().chordName==types[num] &&
			planet.GetComponent<ChordPlanet>().baseNote==notes[num];
	}

	public void RemovePlaced(){
		placed --;
	}

	protected void DisablePlanets(){
		foreach(GameObject planet in planets)
			planet.GetComponent<ChordPlanet>().DisablePlanet();
	}

	public void setRadiusPlanets(float[] radius){
        textField.GetComponent<Text>().text = "Place the planets";
		int ind=0;
		float offset=Screen.height*6/100;
		foreach(GameObject planet in planets){
			planet.SetActive(true);
			planet.GetComponent<Rotate>().SetDirtyOffset();
			planet.GetComponent<Drag>().radiusOrbits=radius;
			switch(ind){
				case 0: planet.transform.position=Camera.main.ScreenToWorldPoint(new Vector3(offset, Screen.height - (offset*3), 1f)); break;
				case 1: planet.transform.position=Camera.main.ScreenToWorldPoint(new Vector3(offset, offset, 1f)); break;
				case 2: planet.transform.position=Camera.main.ScreenToWorldPoint(new Vector3(Screen.width - offset,  Screen.height - (offset*3), 1f)); break;
				case 3: planet.transform.position=Camera.main.ScreenToWorldPoint(new Vector3(Screen.width - offset,  offset, 1f)); break;
				case 4: planet.transform.position=Camera.main.ScreenToWorldPoint(new Vector3(offset, Screen.height/2 - offset, 1f)); break;
				case 5: planet.transform.position=Camera.main.ScreenToWorldPoint(new Vector3(Screen.width - offset,  Screen.height/2 - offset, 1f)); break;
				default: break;
			}
			ind++;
		}

		if(autocomplete)
			Autocomplete();
	}

	// Here we will put a collapsing animation
	public void CollapsePlanets() {
		isWaiting=false;
		currentBar=bars-1;
		Destroy(actualWave);

		foreach(GameObject planet in planets) {

			// Pass the restore positions method to the collapse script
			planet.GetComponent<ChordPlanet>().active=false;
			planet.GetComponent<Collapse>().enabled=true;
			planet.GetComponent<Collapse>().SetRestore(proxyRestore);
			planet.GetComponent<Drag>().enabled=true;
			planet.GetComponent<CircleCollider2D>().radius=2.5f;
			planet.GetComponent<Drag>().orbitNumber=-1;
		}
	}

	public void NextLevel() {
		star.GetComponent<BeatGen>().Unsubscribe(this);
		Destroy(actualWave);
		GetComponent<LevelManager>().goToNextLevel();
	}

	public void proxyRestore(){
		GetComponent<LevelManager>().ResetBackground();
		restoreCount++;
		if(restoreCount==planets.Length)
			RestorePositions();
	}

	// Reset all the planets to their initial states and positions
	void RestorePositions() {

		foreach(GameObject planet in planets) {
			planet.SetActive(false);
			planet.GetComponent<Collapse>().enabled=false;
			planet.GetComponent<Rotate>().enabled=false;
			planet.GetComponent<SelfRotate>().enabled=false;
		}

		restoreCount=0;

		placed=0;
	}

	// This is for testing purposes only
	public void Autocomplete(){
		// [TODO] fix Bug: if you have two orbitS for the Same note,
		// this will Set two planets in the first orbit
		foreach (GameObject planet in planets)
		{
				// Find the correct orbit comparing the note
				int index=0;
				foreach (string note in types){
					if(planet.GetComponent<ChordPlanet>().chordName==note)
						if(planet.GetComponent<ChordPlanet>().baseNote==notes[index])
								break;
					index++;
				}

				// Copied Drag On Mouse Up function for each planet
				float orbit = planet.GetComponent<Drag>().radiusOrbits[index];

				planet.GetComponent<Rotate>().enabled=true;
				planet.GetComponent<Rotate>().SetRadius(orbit);
				planet.GetComponent<Rotate>().SetDirtyOffset();
				planet.GetComponent<SelfRotate>().enabled=true;
				planet.GetComponent<CircleCollider2D>().radius=1f;
				planet.GetComponent<ChordPlanet>().active=true;
				planet.GetComponent<Drag>().enabled=false;
		}
		wave.GetComponent<Wave>().DestroyPreviews();
		NextLevel();
	}

	public int GetNumberOfThirdBeat(){
		return numberOfThirdBeat;
	}

	public void Restart(){

		isPreview=false;
		isWaiting=false;
		int ind=0;
		float offset=Screen.height*6/100;
		foreach(GameObject planet in planets){
			planet.GetComponent<Rotate>().enabled=false;
			planet.GetComponent<SelfRotate>().enabled=false;
			planet.GetComponent<Drag>().enabled=true;
			planet.GetComponent<Drag>().orbitNumber=-1;
			planet.GetComponent<ChordPlanet>().active=false;
			planet.GetComponent<CircleCollider2D>().radius=2.5f;
			switch(ind){
				case 0: planet.transform.position=Camera.main.ScreenToWorldPoint(new Vector3(offset, Screen.height - (offset*3), 1f)); break;
				case 1: planet.transform.position=Camera.main.ScreenToWorldPoint(new Vector3(offset, offset, 1f)); break;
				case 2: planet.transform.position=Camera.main.ScreenToWorldPoint(new Vector3(Screen.width - offset,  Screen.height - (offset*3), 1f)); break;
				case 3: planet.transform.position=Camera.main.ScreenToWorldPoint(new Vector3(Screen.width - offset,  offset, 1f)); break;
				case 4: planet.transform.position=Camera.main.ScreenToWorldPoint(new Vector3(offset, Screen.height/2 - offset, 1f)); break;
				case 5: planet.transform.position=Camera.main.ScreenToWorldPoint(new Vector3(Screen.width - offset,  Screen.height/2 - offset, 1f)); break;
				default: break;
			}
			ind++;
		}
		placed=0;
	}

	public void PlayPreview(){
		if(GetComponent<LevelManager>().GetLevel()==2 && wave.activeSelf==false){
			currentBar=0;
			wave.SetActive(true);
			wave.GetComponent<Wave>().Restart();
		}
	}

	GameObject GetFirstOrbitPlanet(){
		foreach (GameObject planet in planets)
			if(planet.GetComponent<Drag>().orbitNumber==0)
				return planet;

		return null;
	}

}
