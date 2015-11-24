﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using LibPDBinding;

public class Level1 : Subscriber {

	public GameObject[] drums;
	public GameObject star;
	public GameObject textField;

	GameObject currentInstrument;

	int beatsPerBar = 4;
	int subBeatsPerBeat = 4;
	int granularity = 0;
	int currentIndex=0;
	int totalBeats=0;

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
		currentInstrument.GetComponent<Drum>().SetActiveness(true);
		targetDrumArray = currentInstrument.GetComponent<Drum>().targetDrumArray;

		for(int i=0; i<granularity; i++)
			if(targetDrumArray[i])
				totalBeats++;

		LibPD.SendBang("highBeat");

	}

	// Update is called once per frame
	void Update () {

			currentInstrument.GetComponent<Drum>().widenEffect(correctness);



	}

	// This method is called for each beat
	public override void Beat(int currentSlot) {

		// check every bar if the array is correct
		if(currentSlot%subBeatsPerBeat == 0){
			CompareArrays();

			if(currentSlot == 0)
				LibPD.SendBang("highBeat");
			else
					LibPD.SendBang("lowBeat");
				}

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
			StartCoroutine(ShowMessage("Stage Passed!", 2));
			currentIndex++;
			currentInstrument.transform.localScale= new Vector3 (1,1,1);
			currentInstrument.GetComponent<Drum>().SetActiveness(false);
			currentInstrument.GetComponent<CenterRotation>().enabled=true;

			if((currentIndex < drums.Length)){

				string oldState=currentInstrument.GetComponent<Drum>().GetCurrentState();
				currentInstrument=drums[currentIndex];
				drums[currentIndex].GetComponent<Drum>().SetCurrentState(oldState);
				currentInstrument.GetComponent<Drum>().SetActiveness(true);
				targetDrumArray = currentInstrument.GetComponent<Drum>().targetDrumArray;
				totalBeats=0;
				for(int i=0; i<granularity; i++)
					if(targetDrumArray[i])
						totalBeats++;

			} else{
				star.GetComponent<BeatGen>().Unsubscribe(this);
				GetComponent<LevelManager>().goToNextLevel();
			}

	}

	IEnumerator ShowMessage (string message, float delay) {
		 textField.GetComponent<Text>().text = message;
		 textField.SetActive(true);
		 yield return new WaitForSeconds(delay);
		 textField.SetActive(false);
	}

}