using UnityEngine;
using System.Collections;

public class Level2 : Subscriber {

	public GameObject cam;
	public GameObject star;
	public GameObject wave;
	public GameObject wavePrefab;
	public GameObject prev;
	public bool autocomplete=false;
	private int numberOfThirdBeat=0;
	private int currentBar=0;
	public int bars=4;
	public GameObject[] planets;
	private int score;
	public int[] notes = new int[4] {50,50,50,55};
	public string[] types = new string[4] {"M", "m", "M7", "M7"};
	private Vector3[] initialPositions = new Vector3[4];
	int restoreCount=0;
	bool isWaiting=false;


	// Use this for initialization
	void Start () {

		cam.GetComponent<SmoothCamera>().enabled = true;

		cam.GetComponent<SmoothCamera>().setArrival(10.5f);

		star.GetComponent<BeatGen>().Subscribe(this);

		prev.GetComponent<HarmonyPreview>().setPreview(notes, types);

		wave.SetActive(true);

		numberOfThirdBeat=star.GetComponent<BeatGen>().granularity-star.GetComponent<BeatGen>().subBeatsPerBeat;

		SaveInitialPositions();

	}

	// Update is called once per frame
	void Update () {

	}

	// This method is called for each beat
	public override void Beat(int currentSlot) {

		if(currentSlot==numberOfThirdBeat){
			if (currentBar==bars) {
				Instantiate(wavePrefab, new Vector3(0, 0, 0), Quaternion.identity);
				currentBar=0;
				score=0;
				int placed = 0;
				foreach(GameObject planet in planets)
					if(planet.GetComponent<Drag>().orbitNumber!=-1) {
						placed++;
						if(planet.GetComponent<ChordPlanet>().chordName==types[planet.GetComponent<Drag>().orbitNumber])
							score++;
					}
				if(isWaiting) {
					// The player has placed all the planets check the score
					if(score < notes.Length)
						CollapsePlanets();
					else
						NextLevel();
				}

				if(placed == notes.Length)
							isWaiting=true;
				else
							isWaiting=false;
			}
			currentBar++;
		}



	}

	public void setRadiusPlanets(float[] radius){
		foreach(GameObject planet in planets){
			planet.SetActive(true);
			planet.GetComponent<Drag>().radiusOrbits=radius;
		}

		if(autocomplete)
			Autocomplete();
	}

	// Here we will put a collapsing animation
	public void CollapsePlanets() {
		foreach(GameObject planet in planets) {
			// Pass the restore positions method to the collapse script
			planet.GetComponent<Collapse>().SetRestore(proxyRestore);
			planet.GetComponent<Collapse>().enabled=true;
		}
		score = 0;
	}

	public void NextLevel() {
		GetComponent<LevelManager>().goToNextLevel();
	}

	// Saves the initial position of the planets in an array
	private void SaveInitialPositions() {
		int i = 0;
		foreach(GameObject planet in planets) {
			initialPositions[i] = planet.transform.position;
			i++;
		}
	}

	public void proxyRestore(){
		restoreCount++;
		if(restoreCount==planets.Length)
			RestorePositions();
	}

	// Reset all the planets to their initial states and positions
	void RestorePositions() {

		int i = 0;

		foreach(GameObject planet in planets) {
			// Disable the rotation
			planet.GetComponent<Rotate>().enabled=false;
			// Put the planet in its initial position
			planet.transform.position = initialPositions[i];
			// Resets the orbit of the planet
			planet.GetComponent<Drag>().orbitNumber=-1;
			// Makes the planet mute again
			planet.GetComponent<ChordPlanet>().active=false;
			// Stops the planet revolution
			planet.GetComponent<SelfRotate>().enabled=false;
			i++;
		}

		restoreCount=0;
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

				planet.GetComponent<Rotate>().SetRadius(orbit);
				planet.GetComponent<Rotate>().SetDirtyOffset();
				planet.GetComponent<Rotate>().enabled=true;
				planet.GetComponent<SelfRotate>().enabled=true;
				planet.GetComponent<CircleCollider2D>().radius=1f;
				planet.GetComponent<ChordPlanet>().active=true;
				planet.GetComponent<Drag>().enabled=false;
		}
		NextLevel();
	}

}
