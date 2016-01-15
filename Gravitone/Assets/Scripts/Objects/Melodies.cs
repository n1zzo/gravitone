using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Melodies : Subscriber {

	public GameObject star;
	public GameObject audioManager;
	public GameObject[] satellitePrefab;
	public GameObject satelliteDark;
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
			bars[count].tag = "Bar";

			if(count!=0)
				bars[count].SetActive(false);

			count++;
		}

		// Push the preview toggle action to the planets
		PushPreviewActions();

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
					Verify();

			} else {

					currentBar++;

					if(currentBar>3)
						currentBar=0;

			}
		}

		this.currentSlot = currentSlot;

		// Find current index of array
		index = currentSlot+currentBar*granularity;

		// If the preview is active play the target melody
		if(preview) {
			noteToPlay = melodyNotes[index];
			// Place the dark satellite (the second parameter is the radius)
			if(noteToPlay != 0)
				PlaceSatellite(noteToPlay, GetIndex(noteToPlay));
		}

		// Else play the user's recorded melody
		else
			noteToPlay = playerNotes[index];

		// If there was a note...play it!
		if(noteToPlay != 0)
			audioManager.GetComponent<AudioManager>().PlayStrings(noteToPlay);

		if(currentSlot==granularity-1)
			preview=false;

	}

	public void RecordNote(int number) {

		preview=false;

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

	private int GetIndex(int note){
		for (int i=0; i<7; i++)
			if(notes[i]==note)
				return i;

		return -1;
	}

	private void Verify() {
		int matching = 0;
		for (int i=currentBar*granularity ; i<(granularity + currentBar*granularity); i++)
				if(melodyNotes[i]!=0 && melodyNotes[i]==playerNotes[i]) {
					// A note is the correct one in the correct place
					matching++;
					// Make the corresponding orbit opaque
					int o = GetSatellite(playerNotes[i]);
					planets[currentPlanet].GetComponent<ChordPlanet>().SetOpaqueOrbit(o);
				}
				else if(melodyNotes[i]==0 && playerNotes[i]!=0)
					matching--;

		correctness = matching / totalNotes;

		if(correctness==1)
			NextBar();
	}

	// Get the number of the satellite by its played note
	private int GetSatellite(int target) {
		int i = 0;
		foreach (int note in notes) {
			if (note == target)
				return i;
			i++;
		}
		return -1;
	}

	public void NextBar() {
		currentBar++;
		if(currentBar!=4){
			planets[currentPlanet].SetActive(false);
			bars[currentPlanet].SetActive(false);
			calculateTotalNotes(false);
			currentPlanet++;
			planets[currentPlanet].SetActiveRecursively(true);
			bars[currentPlanet].SetActive(true);
			levelManager.GetComponent<Level3>().ChangeCamera(currentPlanet);

			// Push the preview toggle action to the planets
			PushPreviewActions();

			TogglePreview();

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

	public int GetCurrentPlanetNum(){
		return currentPlanet;
	}

	private void PlaceSatellite(int note, int number) {
		if(playerNotes[index]!=0 && !completed){
			Destroy(satellites[index]);
		}

		// Select the prefab of the satellite that will be placed
		GameObject prefab;
		if(preview){
			prefab = satelliteDark;
			Destroy(satellites[index]);
		}
		else
			prefab = satellitePrefab[number];

    GameObject newSatellite = Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
		newSatellite.GetComponent<SatRotate>().initialProgress=star.GetComponent<BeatGen>().progress;
		newSatellite.GetComponent<SatRotate>().index=index;

		float radius = 2f + (number*0.25f);

		// Set satellite rotation parameters
		newSatellite.GetComponent<SatRotate>().star = this.star;
		newSatellite.GetComponent<SatRotate>().planet = planets[currentPlanet];
		newSatellite.GetComponent<SatRotate>().SetRadius(radius);
    satellites[index]=newSatellite;
  }

	public void DestroySat(int index){
		Destroy(satellites[index]);
		playerNotes[index] = 0;
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
			planet.SetActive(true);
			planet.GetComponent<CircleCollider2D>().radius=1f;
			bars[count].SetActive(true);
			count++;
		}


		foreach (GameObject satellite in satellites) {

			if(satellite && satellite.activeSelf)
				satellite.GetComponent<SpriteRenderer>().enabled=true;

		}
	}

	private void PushPreviewActions() {
		planets[currentPlanet].GetComponent<CircleCollider2D>().radius=2.5f;
		planets[currentPlanet].GetComponent<Buttonize>().action = TogglePreview;
	}

	public void TogglePreview() {

		preview = true;
		star.GetComponent<BeatGen>().progress=1f;

	}
}
