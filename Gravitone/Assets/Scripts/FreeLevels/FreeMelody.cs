using UnityEngine;
using System.Collections;

public class FreeMelody : Subscriber {

	public GameObject star;
	public GameObject audioManager;
	public GameObject[] satellitePrefab;
	public GameObject satelliteDark;
	public GameObject levelManager;
	public GameObject[] planets;
	public int[] notes= new int[7];
	private int[] playerNotes = new int[64];
	private int currentSlot;
	private int currentBar;
	private int index;
	private int totalNotes;
	private int granularity;
	private int currentPlanet;
	private GameObject[] satellites = new GameObject[64];
	private bool completed=false;
	public GameObject barPrefab;
	private GameObject[] bars= new GameObject[6];

	// Use this for initialization
	void Start () {

		// Subscribe to the star
		star.GetComponent<BeatGen>().Subscribe(this);

		granularity=star.GetComponent<BeatGen>().granularity;

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

		}

		this.currentSlot = currentSlot;

		// Find current index of array
		index = currentSlot+currentBar*granularity;

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

	private int GetIndex(int note){
		for (int i=0; i<7; i++)
			if(notes[i]==note)
				return i;

		return -1;
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
			currentPlanet++;
			planets[currentPlanet].SetActiveRecursively(true);
			bars[currentPlanet].SetActive(true);
			levelManager.GetComponent<FreeLevel3>().ChangeCamera(currentPlanet);
			DestroySatellites();
		} else {

			completed=true;
			RestoreSatellites();
			levelManager.GetComponent<FreeLevel3>().NextLevel();
			levelManager.GetComponent<FreeLevel2>().enabled=true;
			star.GetComponent<BeatGen>().Subscribe(levelManager.GetComponent<FreeLevel2>());
			levelManager.GetComponent<FreeLevel2>().currentBar=1;
			currentBar=0;
		}

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

	private void DestroySatellites(){
		foreach (GameObject satellite in satellites) {

			if(satellite)
				satellite.GetComponent<SpriteRenderer>().enabled=false;

		}
	}

}
