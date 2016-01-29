using UnityEngine;
using System.Collections;

public class FreeLevel2 : Subscriber {

	public GameObject cam;
	public GameObject menucam;
	public GameObject star;
	public GameObject wave;
	public GameObject wavePrefab;
	public GameObject shuffle;
	public int numberOfThirdBeat=0;
	public int currentBar=0;
	public int bars=4;
	public GameObject[] planets;
	private int score;
	private bool[] placed;
	public int[] notes;
	public string[] types;
	bool isListening=false;
	int restoreCount=0;
	GameObject actualWave;
  GameObject audioManager;
	public GameObject NextButton;

	// Use this for initialization
	void Start () {

    audioManager = GetComponent<FreeLevelManager>().audioManager;

		audioManager.GetComponent<AudioManager>().GetDrums();

		// Set intruments gain levels
		audioManager.GetComponent<AudioManager>().SetDrumVolume(0.3f);
		audioManager.GetComponent<AudioManager>().SetChordsVolume(1f);

		// Adjust the main cam and menu cam to the second level position
		cam.GetComponent<SmoothCamera>().enabled = true;
		cam.GetComponent<SmoothCamera>().setArrival(10.5f);
		menucam.GetComponent<SmoothCamera>().enabled = true;
		menucam.GetComponent<SmoothCamera>().setArrival(10.5f);

		star.GetComponent<BeatGen>().Subscribe(this);

		wave.SetActive(true);

		numberOfThirdBeat=star.GetComponent<BeatGen>().granularity-(star.GetComponent<BeatGen>().subBeatsPerBeat);

		NextButton.SetActive(false);

		notes= new int[4];

		placed=new bool[4];

		shuffle.SetActive(true);

	}

	// Update is called once per frame
	void Update () {

	}

	// This method is called for each beat
	public override void Beat(int currentSlot) {

		if(currentSlot==numberOfThirdBeat){
			if (currentBar==bars) {

				actualWave=Instantiate(wavePrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;

				currentBar=0;

			}
			currentBar++;
		}

	}

	public void increasePlaced(int number){
		placed[number]=true;

		bool allPlaced=true;

		foreach(bool p in placed)
			if(!p){
				allPlaced=false;
				break;
			}

		if(allPlaced){
			shuffle.SetActive(false);
			isListening=true;
			planets[0].GetComponent<ChordPlanet>().Stop();
			foreach (GameObject planet in planets){
				if(planet.GetComponent<FreeDrag>().orbitNumber!=-1)
					planet.GetComponent<ChordPlanet>().active=true;
			}
			GetComponent<FreeLevelManager>().SetGreyBackground();
			NextButton.SetActive(true);
		}
	}

	public bool IsPlaced(int number){
		return placed[number];
	}


	bool CheckPlanet(GameObject planet){
		int num=planet.GetComponent<FreeDrag>().orbitNumber;

		return planet.GetComponent<ChordPlanet>().chordName==types[num] &&
			planet.GetComponent<ChordPlanet>().baseNote==notes[num];
	}

	public void RemovePlaced(int number){
		placed[number]=false;
		shuffle.SetActive(true);
		if(isListening){
			isListening=false;
			planets[0].GetComponent<ChordPlanet>().Stop();
			foreach (GameObject planet in planets){
				planet.GetComponent<ChordPlanet>().active=false;
			}
			GetComponent<FreeLevelManager>().ResetBackground();
			NextButton.SetActive(false);
		}
	}

	protected void DisablePlanets(){
		foreach(GameObject planet in planets)
			planet.GetComponent<ChordPlanet>().DisablePlanet();
	}

	public void setRadiusPlanets(float[] radius){
		int ind=0;
		float offset=Screen.height*6/100;
		foreach(GameObject planet in planets){
			planet.SetActive(true);
			planet.GetComponent<FreeDrag>().radiusOrbits=radius;
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

	}


	public void NextLevel() {
		foreach(GameObject planet in planets){
			int orbitNumber= planet.GetComponent<FreeDrag>().orbitNumber;
			if(orbitNumber!=-1)
				notes[orbitNumber]=planet.GetComponent<ChordPlanet>().baseNote;
		}
		Destroy(actualWave);
		star.GetComponent<BeatGen>().Unsubscribe(this);
		GetComponent<FreeLevelManager>().goToNextLevel();
	}

	public void proxyRestore(){
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

		placed=new bool[4];

	}

	public void Shuffle(){
		foreach(GameObject planet in planets)
			if(planet.GetComponent<FreeDrag>().orbitNumber==-1)
				planet.GetComponent<RandomizeNote>().Randomize();
	}

	public int GetNumberOfThirdBeat(){
		return numberOfThirdBeat;
	}

	public void Restart(){
		int ind=0;
		float offset=Screen.height*6/100;
		foreach(GameObject planet in planets){
			planet.GetComponent<Rotate>().enabled=false;
			planet.GetComponent<SelfRotate>().enabled=false;
			planet.GetComponent<FreeDrag>().enabled=true;
			planet.GetComponent<FreeDrag>().orbitNumber=-1;
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
		placed=new bool[4];
	}

	GameObject GetFirstOrbitPlanet(){
		foreach (GameObject planet in planets)
			if(planet.GetComponent<FreeDrag>().orbitNumber==0)
				return planet;

		return null;
	}

}
