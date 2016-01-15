using UnityEngine;
using System.Collections;

public class FreeLevel1 : Subscriber {

	public GameObject[] drums;
	public GameObject star;
	public GameObject[] dotPrefab;
	public GameObject canvas;
    public GameObject metronome;

	GameObject audioManager;
	GameObject currentInstrument;

	int beatsPerBar = 4;
	int subBeatsPerBeat = 4;
	int granularity = 0;
	int currentIndex;
	int totalBeats;

	//indicates our State. -1 is the first Start
	int barNumber;

	bool[] playerDrumArray;
	bool checkInput;


	// Use this for initialization
	void Start () {

		star.GetComponent<BeatGen>().Subscribe(this);

		beatsPerBar = star.GetComponent<BeatGen>().beatsPerBar;
		subBeatsPerBeat = star.GetComponent<BeatGen>().subBeatsPerBeat;
		granularity = beatsPerBar * subBeatsPerBeat;

		currentInstrument=drums[currentIndex];
		currentInstrument.SetActive(true);
		currentInstrument.GetComponent<Drum>().SetActiveness(true);

		audioManager = GetComponent<FreeLevelManager>().audioManager;
		currentInstrument.GetComponent<Drum>().widenEffect(0.95f);

        metronome.GetComponent<MetroDot>().PlaceDots(beatsPerBar, subBeatsPerBeat, 4f);
	}

	// Update is called once per frame
	void Update () {

			if(currentInstrument.GetComponent<Drum>().CheckFire()){

				// call the Recording drum state and returns the memorized slot
				int slot = currentInstrument.GetComponent<Drum>().UpdateRecord();

				metronome.GetComponent<MetroDot>().ColorDot(slot);

				metronome.GetComponent<MetroDot>().HitDot(slot);

			}

			//call the function to give a limit to the planet's size according to the correctness
			currentInstrument.GetComponent<Drum>().widenEffect(0.95f);

	}

	// This method is called for each beat
	public override void Beat(int currentSlot) {

		// Fill the current metronome dot
		metronome.GetComponent<MetroDot>().FillDot(currentSlot);

		if(currentInstrument.GetComponent<Drum>().GetDrumArray()[currentSlot])
			metronome.GetComponent<MetroDot>().HitDot(currentSlot);

		if ( currentSlot==0) {
		    audioManager.GetComponent<AudioManager>().HighBeat();
		}
		else if(currentSlot%subBeatsPerBeat==0)
		    audioManager.GetComponent<AudioManager>().LowBeat();
	}


	public void Cancel(){
		currentInstrument.GetComponent<Drum>().Cancel();
	}



	public void ChangeState() {

			currentIndex++;
			currentInstrument.transform.localScale= new Vector3 (1,1,1);
			currentInstrument.GetComponent<Drum>().SetActiveness(false);
			currentInstrument.GetComponent<CenterRotation>().enabled=true;

			if((currentIndex < drums.Length)){
				metronome.GetComponent<MetroDot>().ResetPink();
				currentInstrument=drums[currentIndex];
				currentInstrument.SetActive(true);
				currentInstrument.GetComponent<Drum>().SetActiveness(true);
				currentInstrument.GetComponent<Drum>().widenEffect(0.95f);

			} else{

				foreach(GameObject drum in drums)
					drum.GetComponent<Drum>().SetSecondPhase();
				metronome.GetComponent<MetroDot>().DestroyAll();
				canvas.SetActive(false);
				star.GetComponent<BeatGen>().Unsubscribe(this);
				GetComponent<FreeLevelManager>().goToNextLevel();
			}

	}

	public void Restart(){
		star.GetComponent<BeatGen>().Unsubscribe(this);

		foreach (GameObject drum in drums){
			drum.SetActive(false);
			drum.GetComponent<Drum>().Reset();
		}

		star.GetComponent<BeatGen>().progress=0;
		currentIndex=0;
		Start();
	}

	public void Redo(){
		currentInstrument.GetComponent<Drum>().Reset();
		currentInstrument.GetComponent<Drum>().SetActiveness(true);
		metronome.GetComponent<MetroDot>().ResetPink();
	}

}
