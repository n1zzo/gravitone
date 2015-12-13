using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using LibPDBinding;

public class Level1 : Subscriber {

	public GameObject[] drums;
	public GameObject star;
	public GameObject textField;
	public GameObject trail;
	public GameObject[] dotPrefab;

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
	bool[] targetDrumArray;
	bool countdown;
	bool checkInput;

	float correctness;

	// Use this for initialization
	void Start () {

		totalBeats=0;
		barNumber=-1;
		correctness=0;
		countdown=false;
		currentIndex=0;

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

			if(currentInstrument.GetComponent<Drum>().CheckFire()){

								// States 4 and 3 are RECORDING States
								if ( barNumber==4 || barNumber==3) {

									// call the Recording drum state and returns the memorized slot
									int slot=currentInstrument.GetComponent<Drum>().UpdateRecord();

									/* We create a Dot and calculate the correctness according to
									the effective correctness*/
									if(targetDrumArray[slot]){

											Instantiate(dotPrefab[2], trail.transform.position, Quaternion.identity);
											correctness += 1/(float)totalBeats;

										}
										else {
											Instantiate(dotPrefab[1], trail.transform.position, Quaternion.identity);
											if(correctness>0){
													correctness -= 1/(float)totalBeats;
											}
										}

									/*5 is first listen state, then there are the other listen States */
									} else if ( barNumber>5){

												Cancel();

								//State 0 is the preview state. We go on only when there is a TAP
								} else if ( barNumber==0) {

									barNumber++;
									star.GetComponent<BeatGen>().progress=0.97f;

								}

								// recognize if there has Been a Tap from when it was false
								checkInput=true;
			}

			//call the function to give a limit to the planet's size according to the correctness
			currentInstrument.GetComponent<Drum>().widenEffect(correctness);

	}

	// This method is called for each beat
	public override void Beat(int currentSlot) {

			//Handle the Countdown if we are in Countdown State
			if(barNumber==2){

				int beat=Mathf.RoundToInt(currentSlot/beatsPerBar);

				if(countdown)
					textField.GetComponent<Text>().text = (beatsPerBar - beat).ToString();



					// here we are passing to state 3, the PRE-recording state
				if(currentSlot==granularity-1){
					barNumber++;
				}	else if(currentSlot==granularity-3){
					if(!trail.activeSelf)
						trail.GetComponent<Trail>().SetInitialY();
					trail.SetActive(true);
				}
			}


			if ( currentSlot==0) {

				audioManager.GetComponent<AudioManager>().HighBeat();

				if(barNumber!=0)
					barNumber++;

				// Skip number 1 because we will increase the barNumber 2 times for the same state
				// State 3 only corrects a quantization issue
				switch(barNumber){

					// Set Preview State
					case 0:
						trail.SetActive(true);
						trail.GetComponent<Trail>().SetInitialPosition();
						SetPlayPreview();
						textField.GetComponent<Text>().text = "Tap to Play!";
						break;

					// Set Countdown State
					case 2:
						SetStopPreview();
						countdown=true;
						trail.SetActive(false);
						break;

					// Set Recording State
					case 4:
						textField.GetComponent<Text>().text = "";
						countdown=false;
						GetComponent<LevelManager>().SetGreenBackground();
						checkInput=false;
						SetRecord();
						break;

					// Set listen State
					case 5:
						if(checkInput){
							star.GetComponent<BeatGen>().progress=0f;
							SetRecord();
							CompareArrays();
						} else {
							trail.SetActive(false);
							barNumber=2;
							countdown=true;
							GetComponent<LevelManager>().ResetBackground();
						}
						break;
				}
			}

			else if(currentSlot%subBeatsPerBeat==0)
					audioManager.GetComponent<AudioManager>().LowBeat();

			// SetS the target drum dots
			if(trail.activeSelf && targetDrumArray[currentSlot])
					Instantiate(dotPrefab[0], trail.transform.position, Quaternion.identity);

			//on the listen States we must reinsert the dots according to player's drum
			if(barNumber>4){

				if(playerDrumArray[currentSlot] && targetDrumArray[currentSlot])
						Instantiate(dotPrefab[2], trail.transform.position, Quaternion.identity);

				else if(playerDrumArray[currentSlot])
					Instantiate(dotPrefab[1], trail.transform.position, Quaternion.identity);

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
		correctness=0;
		barNumber=-1;
		currentInstrument.GetComponent<Drum>().Cancel();
		trail.SetActive(false);
		GetComponent<LevelManager>().ResetBackground();
	}

	void CompareArrays() {
		playerDrumArray=currentInstrument.GetComponent<Drum>().GetDrumArray();

			if(correctness>0.95 || totalBeats==0)
				ChangeState();
			else
				GetComponent<LevelManager>().SetRedBackground();

	}

	void ChangeState() {
			GetComponent<LevelManager>().ResetBackground();
			trail.SetActive(false);
			textField.GetComponent<Text>().text = "Amazing!";
			currentIndex++;
			currentInstrument.transform.localScale= new Vector3 (1,1,1);
			currentInstrument.GetComponent<Drum>().SetActiveness(false);
			currentInstrument.GetComponent<CenterRotation>().enabled=true;

			if((currentIndex < drums.Length)){
				correctness=0;
				barNumber=-1;
				currentInstrument=drums[currentIndex];
				currentInstrument.SetActive(true);
				currentInstrument.GetComponent<Drum>().SetActiveness(true);
				targetDrumArray = currentInstrument.GetComponent<Drum>().targetDrumArray;
				totalBeats=0;

				for(int i=0; i<granularity; i++)
					if(targetDrumArray[i])
						totalBeats++;

			} else{
				foreach(GameObject drum in drums)
					drum.GetComponent<Drum>().SetSecondPhase();

				textField.GetComponent<Text>().text = "";
				star.GetComponent<BeatGen>().Unsubscribe(this);
				GetComponent<LevelManager>().goToNextLevel();
			}

	}

	//for testing purposes only
	public void Autocomplete(){
		foreach (GameObject drum in drums){
			drum.SetActive(true);
			drum.GetComponent<Drum>().Autocomplete();
			currentInstrument=drum;
			correctness = 1;
			ChangeState();
		}
	}

	/*IEnumerator ShowMessage (string message, float delay) {
		 textField.GetComponent<Text>().text = message;
		 textField.SetActive(true);
		 yield return new WaitForSeconds(delay);
		 textField.SetActive(false);
	}*/

	public void Restart(){
		star.GetComponent<BeatGen>().Unsubscribe(this);
		correctness = 0;
		foreach (GameObject drum in drums){
			drum.SetActive(false);
			drum.GetComponent<Drum>().Reset();
		}
		star.GetComponent<BeatGen>().progress=0;
		Start();
	}

}
