using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Melodies : Subscriber {

	public GameObject star;
	public GameObject audioManager;
	public GameObject satellitePrefab;
	public GameObject levelManager;
	public GameObject[] planets;
	public int[] notes= new int[7];
	public int[] melodyNotes = new int[64];
	private int[] playerNotes = new int[64];
	private int currentSlot;
	private int currentBar;
	private int index;
	private float correctness = 0;
	private int totalNotes;
	private int granularity;
	private int currentPlanet;
	private GameObject[] satellites = new GameObject[64];
	private bool completed=false;
	public GameObject barPrefab;
	private GameObject[] bars= new GameObject[6];
	private bool preview=false;


	// Use this for initialization
	void Start () {

		// Subscribe to the star
		star.GetComponent<BeatGen>().Subscribe(this);

		granularity=star.GetComponent<BeatGen>().granularity;

		calculateTotalNotes(false);

		currentPlanet=0;

		int count=0;

		foreach (GameObject planet in planets){
			bars[count]= Instantiate(barPrefab, Vector3.zero, Quaternion.identity) as GameObject;
			bars[count].GetComponent<Bars>().planet=planet;

			if(count!=0)
				bars[count].SetActive(false);

			count++;
		}


	}

	// Update is called once per frame
	void Update () {

	}

	// currentSlot ranges from 0 to 15
	public override void Beat(int currentSlot){
		int noteToPlay = 0;

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

		// If the preview is active play the target melody
		if (preview)
			noteToPlay = melodyNotes[index];
		// Else play the user's recorded melody
		else
			noteToPlay = playerNotes[index];
		// If there was a note...play it!
		if(noteToPlay != 0)
			audioManager.GetComponent<AudioManager>().PlayStrings(noteToPlay);
	}

	public void RecordNote(int number) {

		int note=notes[number];

		index = Mathf.RoundToInt(star.GetComponent<BeatGen>().progress * (float) granularity);

		// If it's divided in N, then the Nth beat is the initial 0
		if(index == granularity)
			index = 0;

		// Find current index of array
		index = index+currentBar*granularity;

		playerNotes[index] = note;
		audioManager.GetComponent<AudioManager>().PlayStrings(note);
		PlaceSatellite(note, number);
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
			planets[currentPlanet].SetActiveRecursively(false);
			bars[currentPlanet].SetActive(false);
			calculateTotalNotes(false);
			currentPlanet++;
			planets[currentPlanet].SetActiveRecursively(true);
			bars[currentPlanet].SetActive(true);
			levelManager.GetComponent<Level3>().changeCamera(currentPlanet);
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

	public void calculateTotalNotes(bool destroySat){
		totalNotes=0;

		for (int i=currentBar*granularity ; i<granularity +currentBar*granularity ; i++){
			playerNotes[i]=0;
			if(melodyNotes[i]!=0)
				totalNotes++;
		}

		DeleteSatellites(destroySat);
	}

	public GameObject GetCurrentPlanet(){
		return planets[currentPlanet];
	}

	private void PlaceSatellite(int note, int number) {
		if(playerNotes[index]!=0 && !completed){
			Destroy(satellites[index]);
		}

    Vector3 initialPosition = new Vector3(0, 0, 0);
    GameObject newSatellite = Instantiate(satellitePrefab, initialPosition, Quaternion.identity) as GameObject;

		float radius = 2f + (number*0.25f);

		// Set satellite rotation parameters
		newSatellite.GetComponent<SatRotate>().star = this.star;
		newSatellite.GetComponent<SatRotate>().planet = planets[currentPlanet];
		newSatellite.GetComponent<SatRotate>().SetRadius(radius);
    satellites[index]=newSatellite;
  }

	private void DeleteSatellites(bool destroySat) {

		for(int i=currentPlanet*granularity; i<granularity*(currentPlanet+1); i++) {
			if(satellites[i]){
				if(destroySat)
					satellites[i].SetActive(false);
				else
					satellites[i].GetComponent<SpriteRenderer>().enabled=false;
			}
		}
	}

	private void RestoreSatellites() {
		int count=0;

		foreach(GameObject planet in planets){
			planet.SetActiveRecursively(true);
			bars[count].SetActive(true);
			count++;
		}


		foreach (GameObject satellite in satellites) {
			if(satellite && satellite.activeSelf)
				satellite.GetComponent<SpriteRenderer>().enabled=true;
		}
	}

	private void PushPreviewActions() {
		GameObject currentPlanet = GetCurrentPlanet();
		currentPlanet.GetComponent<Buttonize>().action = TogglePreview;
	}

	public void TogglePreview() {
		this.preview = !this.preview;
	}

}
