using UnityEngine;
using System.Collections;

public class Level2 : Subscriber {

	public GameObject cam;
	public GameObject star;
	public GameObject wave;
	public GameObject wavePrefab;
	int numberOfThirdBeat=0;
	int currentBar=0;
	public int bars=4;

	// Use this for initialization
	void Start () {

		cam.GetComponent<SmoothCamera>().enabled = true;
		cam.GetComponent<SmoothCamera>().setArrival(10.5f);

		star.GetComponent<BeatGen>().Subscribe(this);

		numberOfThirdBeat=star.GetComponent<BeatGen>().granularity-star.GetComponent<BeatGen>().subBeatsPerBeat;

	}

	// Update is called once per frame
	void Update () {

	}

	// This method is called for each beat
	public override void Beat(int currentSlot) {

		if(currentSlot==numberOfThirdBeat){
				wave.SetActive(true);
				if (currentBar==bars) {
					Instantiate(wavePrefab, new Vector3(0, 0, 0), Quaternion.identity);
					currentBar=0;
				}
				currentBar++;
			}

	}
}
