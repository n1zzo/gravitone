using UnityEngine;
using System.Collections;

public class BassPlayer : Subscriber {

	public GameObject kick;
	public GameObject levelManager;
	public GameObject star;
	public GameObject audioManager;
	public GameObject melody;
	int currentBar=-1;
	int bars=4;
	int granularity;

	// Use this for initialization
	void Start () {
		star.GetComponent<BeatGen>().Subscribe(this);
		granularity=star.GetComponent<BeatGen>().granularity;
	}

	// Update is called once per frame
	void Update () {

	}

	// This method is called for each beat
	public override void Beat(int currentSlot) {
		if(currentSlot==0 && currentBar!=bars)
			currentBar++;

		if (currentBar==bars)
			currentBar=0;

		if(kick.GetComponent<Drum>().targetDrumArray[currentSlot] && currentBar!=-1){
			int note;

			if(levelManager.GetComponent<LevelManager>().GetLevel()!=3)
				note=levelManager.GetComponent<Level2>().notes[currentBar];
			else{
				int currentPlanet=melody.GetComponent<Melodies>().GetCurrentPlanetNum();

				note=levelManager.GetComponent<Level2>().notes[currentPlanet];
			}


			while(note>36)
				note-=12;

			audioManager.GetComponent<AudioManager>().PlayBass(note);

		} else if ( kick.GetComponent<Drum>().targetDrumArray[currentSlot+1] || currentSlot==granularity-1 ) {
			audioManager.GetComponent<AudioManager>().StopBass();
		}

	}

	public void ResetCurrentBar(){
		currentBar=0;
	}

}
