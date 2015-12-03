using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using LibPDBinding;

public class Level1 : Subscriber {

	public GameObject[] drums;
	public GameObject star;
	public GameObject textField;
	public GameObject canvas;
	public GameObject trail;
	GameObject audioManager;

	GameObject currentInstrument;

	int beatsPerBar = 4;
	int subBeatsPerBeat = 4;
	int granularity = 0;
	int currentIndex=0;
	int totalBeats=0;
	int barNumber=-1;

	bool[] playerDrumArray;
	bool[] targetDrumArray;
	bool countdown=false;
	bool checkInput;

	float correctness = 0;

	// Use this for initialization
	void Start () {

		canvas.SetActive(true);

		star.GetComponent<BeatGen>().Subscribe(this);
		beatsPerBar = star.GetComponent<BeatGen>().beatsPerBar;
		subBeatsPerBeat = star.GetComponent<BeatGen>().subBeatsPerBeat;
		granularity = beatsPerBar * subBeatsPerBeat;
		currentInstrument=drums[currentIndex];
		currentInstrument.SetActive(true);
		currentInstrument.GetComponent<Drum>().SetActiveness(true);
		targetDrumArray = currentInstrument.GetComponent<Drum>().targetDrumArray;

		for(int i=0; i<granularity; i++)
			if(targetDrumArray[i])
				totalBeats++;

		audioManager = GetComponent<LevelManager>().audioManager;

	}

	// Update is called once per frame
	void Update () {

			currentInstrument.GetComponent<Drum>().widenEffect(correctness);
			if(currentInstrument.GetComponent<Drum>().CheckFire()){
								checkInput=true;
								if ( barNumber>4){
												barNumber=-1;
												Cancel();
												correctness=0;
								} else if ( barNumber==0) {
									barNumber++;
									star.GetComponent<BeatGen>().progress=0.97f;
								}
			}
	}

	// This method is called for each beat
	public override void Beat(int currentSlot) {

		CompareArrays();

		int beat=Mathf.RoundToInt(currentSlot/beatsPerBar);
		if(countdown && beat!=0) {
			textField.GetComponent<Text>().text = (beatsPerBar - beat).ToString();
	 	} else if(countdown)
			textField.GetComponent<Text>().text = "Prepare to Tap!";

			if(currentSlot == granularity-1){

				if(barNumber!=0)
					barNumber++;

				if(barNumber==2){
					SetStopPreview();
					countdown=true;
					trail.SetActive(false);
				}

			} else if ( currentSlot==0) {
				audioManager.GetComponent<AudioManager>().HighBeat();

				// Skip 1 because we will increase the barNumber 2 times
				switch(barNumber){
					case 0: trail.SetActive(true); SetPlayPreview(); textField.GetComponent<Text>().text = "Tap to Play!"; break;
					case 3: trail.SetActive(true); countdown=false; textField.GetComponent<Text>().text = ""; checkInput=false; SetRecord(); break;
					case 4:
						if(checkInput){
							star.GetComponent<BeatGen>().progress=0f;
							SetRecord();
							CompareArrays();
						} else {trail.SetActive(false); barNumber=2; countdown=true;}break;
				}
			}

			else if(currentSlot%subBeatsPerBeat==0)
					audioManager.GetComponent<AudioManager>().LowBeat();

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

			int hit = 0;
			int wrong = 0;

			// This could be optimized
			for(int i=0; i<granularity; i++)
				if(targetDrumArray[i] && playerDrumArray[i])
						hit++;

			 else if(playerDrumArray[i])
						wrong++;

			int balance = hit - Mathf.RoundToInt(wrong/2);

			if(balance > 0)
				correctness = (float)balance/(float)totalBeats;
			else
				correctness = 0;

			if(correctness==1 || totalBeats==0)
				ChangeState();

	}

	void ChangeState() {


			//StartCoroutine(ShowMessage("Stage Passed!", 2));
			currentIndex++;
			currentInstrument.transform.localScale= new Vector3 (1,1,1);
			currentInstrument.GetComponent<Drum>().SetActiveness(false);
			currentInstrument.GetComponent<CenterRotation>().enabled=true;

			if((currentIndex < drums.Length)){
				correctness=0;
				barNumber=-1;
				string oldState=currentInstrument.GetComponent<Drum>().GetCurrentState();
				currentInstrument=drums[currentIndex];
				currentInstrument.SetActive(true);
				drums[currentIndex].GetComponent<Drum>().SetCurrentState(oldState);
				currentInstrument.GetComponent<Drum>().SetActiveness(true);
				targetDrumArray = currentInstrument.GetComponent<Drum>().targetDrumArray;
				totalBeats=0;

				for(int i=0; i<granularity; i++)
					if(targetDrumArray[i])
						totalBeats++;

			} else{

				star.GetComponent<BeatGen>().Unsubscribe(this);
				canvas.SetActive(false);
				trail.SetActive(false);
				GetComponent<LevelManager>().goToNextLevel();

			}

	}

	public void Autocomplete(){
		foreach (GameObject drum in drums)
			drum.GetComponent<Drum>().Autocomplete();
			correctness = 1;
	}

/*	IEnumerator ShowMessage (string message, float delay) {
		 textField.GetComponent<Text>().text = message;
		 textField.SetActive(true);
		 yield return new WaitForSeconds(delay);
		 textField.SetActive(false);
	}*/

}
