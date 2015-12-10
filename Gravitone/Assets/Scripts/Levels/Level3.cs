using UnityEngine;
using System.Collections;

public class Level3 : Subscriber {

	public GameObject cam;
	public GameObject[] planets;
	public GameObject background;
	public GameObject audioManager;
	public GameObject star;
	public GameObject wavePrefab;
	public int currentBar;
	int numberOfThirdBeat;

	// Use this for initialization
	void Start () {


		background.GetComponent<Fade>().enabled=true;
		background.GetComponent<Fade>().final=0.9f;
		background.GetComponent<SpriteRenderer>().enabled=true;
/*
		foreach(GameObject planet in planets){
			planet.GetComponent<SpriteRenderer>().enabled=false;
			planet.GetComponent<Rotate>().enabled=false;
			planet.GetComponent<Drag>().enabled=false;
		}

		// Set the camera and menu to follow the first planet.
		cam.GetComponent<SmoothFollow2D>().target = planets[0].transform;
		cam.GetComponent<SmoothFollow2D>().enabled = true;
		cam.GetComponent<SmoothCamera>().setArrival(5f);
		cam.GetComponent<SmoothCamera>().enabled=true;

		planets[0].GetComponent<SpriteRenderer>().enabled=true;
*/

	star.GetComponent<BeatGen>().Subscribe(this);

	numberOfThirdBeat=star.GetComponent<BeatGen>().granularity-(star.GetComponent<BeatGen>().subBeatsPerBeat);

	}

	// Update is called once per frame
	void Update () {

	}

	public void Restart(){

	}

	// This method is called for each beat
	public override void Beat(int currentSlot) {

		if(currentSlot==0){
				currentBar++;

			if(currentBar>=4)
					currentBar=0;
		}

		if(currentBar==0 && currentSlot==numberOfThirdBeat)
			Instantiate(wavePrefab, new Vector3(0, 0, 0), Quaternion.identity);

		if(currentBar==1 && currentSlot==star.GetComponent<BeatGen>().subBeatsPerBeat){
			audioManager.GetComponent<AudioManager>().PlayMelody();
			Debug.Log("audio");
		}

	}

}
