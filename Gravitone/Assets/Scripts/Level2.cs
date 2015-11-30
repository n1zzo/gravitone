using UnityEngine;
using System.Collections;

public class Level2 : Subscriber {

	public GameObject cam;
	public GameObject star;
	public GameObject wave;
	public GameObject wavePrefab;
	public GameObject prev;
	int numberOfThirdBeat=0;
	int currentBar=0;
	public int bars=4;
	public GameObject[] planets;
	private int score;
	public int[] notes = new int[4] {50,50,50,55};
	public string[] types = new string[4] {"M", "m", "M7", "M7"};

	// Use this for initialization
	void Start () {

		cam.GetComponent<SmoothCamera>().enabled = true;
		cam.GetComponent<SmoothCamera>().setArrival(10.5f);

		star.GetComponent<BeatGen>().Subscribe(this);

		prev.GetComponent<HarmonyPreview>().setPreview(notes, types);

		wave.SetActive(true);

		numberOfThirdBeat=star.GetComponent<BeatGen>().granularity-star.GetComponent<BeatGen>().subBeatsPerBeat;

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
					foreach(GameObject planet in planets)
						if(planet.GetComponent<ChordPlanet>().chordName==types[planet.GetComponent<Drag>().orbitNumber])
							score++;
				}
				currentBar++;
			}
	}

	public void setRadiusPlanets(float[] radius){
		foreach(GameObject planet in planets){
			planet.SetActive(true);
			planet.GetComponent<Drag>().radiusOrbits=radius;
		}
	}

}
