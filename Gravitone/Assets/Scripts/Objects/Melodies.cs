using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Melodies : Subscriber {

	public GameObject star;
	public GameObject audioManager;
	public GameObject satellitePrefab;
	public int[] melodyNotes = new int[64];
	private int[] playerNotes = new int[64];
	private int currentSlot;
	private int currentBar;
	private int index;
	private float correctness = 0;
	private int totalNotes;
	private int granularity;
	public GameObject[] planets;
	private int currentPlanet;
	public GameObject levelManager;
	private GameObject[] satellites = new GameObject[64];
	private bool completed=false;


	// Use this for initialization
	void Start () {

		// Subscribe to the star
		star.GetComponent<BeatGen>().Subscribe(this);

		granularity=star.GetComponent<BeatGen>().granularity;

		calculateTotalNotes();

		currentPlanet=0;

	}

	// Update is called once per frame
	void Update () {

	}

	// currentSlot ranges from 0 to 15
	public override void Beat(int currentSlot){
		if(currentSlot==0){
			if(!completed){

					planets[currentPlanet].GetComponent<ChordPlanet>().Play();

			} else {

					currentBar++;

					if(currentBar>3)
						currentBar=0;

			}
			if(!completed)
				Verify();
		}

		this.currentSlot = currentSlot;

		// Find current index of array
		index = currentSlot+currentBar*granularity;

		// Play the user's saved note
		int noteToPlay = playerNotes[index];
		if(noteToPlay != 0)
			audioManager.GetComponent<AudioManager>().PlayStrings(noteToPlay);
	}

	public void RecordNote(int note) {

		index = Mathf.RoundToInt(star.GetComponent<BeatGen>().progress * (float) granularity);

		// If it's divided in N, then the Nth beat is the initial 0
		if(index == granularity)
			index = 0;

		// Find current index of array
		index = index+currentBar*granularity;

		playerNotes[index] = note;
		audioManager.GetComponent<AudioManager>().PlayStrings(note);
		PlaceSatellite(note);
	}

	private void Verify() {
		int matching = 0;
		for (int i=currentBar*granularity ; i<(granularity + currentBar*granularity); i++)
				if(melodyNotes[i]!=0 && melodyNotes[i]==playerNotes[i])
					matching++;
				else if(melodyNotes[i]==0 && playerNotes[i]!=0)
					matching--;

		correctness = matching / totalNotes;

		if(correctness==1)
			NextBar();
	}

	public void NextBar() {
		currentBar++;
		if(currentBar!=4){
			calculateTotalNotes();
			currentPlanet++;
			levelManager.GetComponent<Level3>().changeCamera(currentPlanet);
			//Verify();
		} else {
			completed=true;
			RestoreSatellites();
			levelManager.GetComponent<Level3>().NextLevel();
			levelManager.GetComponent<Level2>().enabled=true;
			star.GetComponent<BeatGen>().Subscribe(levelManager.GetComponent<Level2>());
			levelManager.GetComponent<Level2>().currentBar=1;
			currentBar=0;
		}
	}

	public void calculateTotalNotes(){
		totalNotes=0;

		for (int i=currentBar*granularity ; i<granularity +currentBar*granularity ; i++){
			playerNotes[i]=0;
			if(melodyNotes[i]!=0)
				totalNotes++;
		}

		DeleteSatellites();
	}

	public GameObject GetCurrentPlanet(){
		return planets[currentPlanet];
	}

	private void PlaceSatellite(int note) {
		if(playerNotes[index]!=0 && !completed)
			Destroy(satellites[index]);

    Vector3 initialPosition = new Vector3(0, 0, 0);
    GameObject newSatellite = Instantiate(satellitePrefab, initialPosition, Quaternion.identity) as GameObject;

		float radius = 2f + ((note%12)*0.25f);

		// Set satellite rotation parameters
		newSatellite.GetComponent<SatRotate>().star = this.star;
		newSatellite.GetComponent<SatRotate>().planet = planets[currentPlanet];
		newSatellite.GetComponent<SatRotate>().SetRadius(radius);
    satellites[index]=newSatellite;
  }

	private void DeleteSatellites() {
		foreach (GameObject satellite in satellites) {
			if(satellite)
				satellite.GetComponent<SpriteRenderer>().enabled=false;
		}
	}

	private void RestoreSatellites() {
		foreach (GameObject satellite in satellites) {
			if(satellite)
				satellite.GetComponent<SpriteRenderer>().enabled=true;
		}
	}


}
