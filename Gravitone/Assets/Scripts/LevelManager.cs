using UnityEngine;
using System.Collections;

public class LevelManager : Subscriber {

	public GameObject[] drums;
	public GameObject star;

	int beatsPerBar = 4;
	int subBeatsPerBeat = 4;
	int granularity = 0;

	GameObject currentInstrument;

	int currentIndex=0;

	bool[] playerDrumArray;
	bool[] targetDrumArray;

	float correctness = 0;

	// Use this for initialization
	void Start () {
		star.GetComponent<BeatGen>().Subscribe(this);
		beatsPerBar = star.GetComponent<BeatGen>().beatsPerBar;
		subBeatsPerBeat = star.GetComponent<BeatGen>().subBeatsPerBeat;
		granularity = beatsPerBar * subBeatsPerBeat;

		currentInstrument=drums[currentIndex];
		targetDrumArray = currentInstrument.GetComponent<Drum>().targetDrumArray;
	}

	// Update is called once per frame
	void Update () {

	}

	// This method is called for each beat
	public override void Beat(int currentSlot) {
		// If one bar is complete, check the arrays completeness
		if (currentSlot == granularity-1)
			CompareArrays();
	}

	public void SetRecord () {
		currentInstrument.GetComponent<Drum>().ToggleRecord();
	}

	public void SetPlayPreview () {
		currentInstrument.GetComponent<Drum>().PlayPreview();
	}

	public void SetStopPreview () {
		currentInstrument.GetComponent<Drum>().StopPreview();
	}

	public void Cancel(){
		currentInstrument.GetComponent<Drum>().Cancel();
	}


	void CompareArrays() {
			playerDrumArray = currentInstrument.GetComponent<Drum>().GetDrumArray();

			int totalBeats = 0;
			int hit = 0;
			int wrong = 0;
			// This could be optimized by checking only the used cells.
			for(int i=0; i<granularity; i++) {
				if(targetDrumArray[i]) {
					totalBeats++;
					if(playerDrumArray[i])
						hit++;
				} else if(playerDrumArray[i])
					wrong++;
			}

			int balance = hit - wrong;

			if(balance > 0)
				correctness = (float)balance/(float)totalBeats;
			else
				correctness = 0;

			if(correctness==1)
				ChangeState();
	}

	void ChangeState(){
			currentIndex++;
			if(drums[currentIndex]){
				string oldState=currentInstrument.GetComponent<Drum>().GetCurrentState();
				currentInstrument=drums[currentIndex];
				drums[currentIndex].GetComponent<Drum>().SetCurrentState(oldState);
				targetDrumArray = currentInstrument.GetComponent<Drum>().targetDrumArray;
			}
	}

}
