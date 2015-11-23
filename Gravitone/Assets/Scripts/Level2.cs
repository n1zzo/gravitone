using UnityEngine;
using System.Collections;

public class Level2 : Subscriber {

	public GameObject cam;
	public GameObject star;
	public GameObject wave;
	int numberOfThirdBeat=0;

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

		if(currentSlot==numberOfThirdBeat)
				wave.SetActive(true);

	}
}
