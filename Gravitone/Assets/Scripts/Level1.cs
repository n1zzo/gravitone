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
								if ( barNumber>2){
												barNumber=-1;
												Cancel();
												correctness=0;
								}
			}
	}

	// This method is called for each beat
	public override void Beat(int currentSlot) {

		CompareArrays();

		// check every bar if the array is correct
		int beat=Mathf.RoundToInt(currentSlot/beatsPerBar);
		if(countdown && beat!=0) {
			textField.GetComponent<Text>().text = (beatsPerBar - beat).ToString();
	 	} else if(countdown)
			textField.GetComponent<Text>().text = "Prepare to Tap!";
		else {
			textField.GetComponent<Text>().text = "";
		}

			if(currentSlot == 0){
					barNumber++;

					switch(barNumber){
						case 0: trail.SetActive(true); SetPlayPreview(); break;
						case 1: SetStopPreview(); countdown=true; trail.SetActive(false); break;
						case 2: trail.SetActive(true); countdown=false; checkInput=false; SetRecord(); break;
						case 3: trail.SetActive(false);
							if(checkInput){
								SetRecord();
								CompareArrays();
							} else {barNumber=1; countdown=true;}break;
					}
					audioManager.GetComponent<AudioManager>().HighBeat();
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
	}

/*	IEnumerator ShowMessage (string message, float delay) {
		 textField.GetComponent<Text>().text = message;
		 textField.SetActive(true);
		 yield return new WaitForSeconds(delay);
		 textField.SetActive(false);
	}*/

}
