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

	bool[] playerDrumArray;
	bool[] targetDrumArray;
	bool countdown;
	bool checkInput;

	float correctness;

	// Use this for initialization
	void Start () {

		totalBeats=0;
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

		trail.SetActive(true);
		trail.GetComponent<Trail>().SetInitialPosition();

	}

	// Update is called once per frame
	void Update () {

			if(currentInstrument.GetComponent<Drum>().CheckFire()){

				if(!checkInput){
					textField.GetComponent<Text>().text = "";
					GetComponent<LevelManager>().SetGreenBackground();
					SetRecord();

					// recognize if there has Been a Tap from when it was false
					checkInput=true;
				}

				// call the Recording drum state and returns the memorized slot
				int slot=currentInstrument.GetComponent<Drum>().UpdateRecord();

				/* We create a Dot and calculate the correctness according to
				the effective correctness*/
				if(targetDrumArray[slot] && checkInput){

					Instantiate(dotPrefab[2], trail.transform.position, Quaternion.identity);
					correctness += 1/(float)totalBeats;
					GetComponent<BloomControl>().BloomPulse();

				}	else if(checkInput) {

					Instantiate(dotPrefab[1], trail.transform.position, Quaternion.identity);
					correctness=0;
					GetComponent<LevelManager>().SetRedBackground();
				}
			}

			//call the function to give a limit to the planet's size according to the correctness
			currentInstrument.GetComponent<Drum>().widenEffect(correctness);

	}

	// This method is called for each beat
	public override void Beat(int currentSlot) {

			if ( currentSlot==granularity-1) {

				if(!checkInput){
					SetPlayPreview();
					textField.GetComponent<Text>().text = "Tap to Play!";
				} else{
					CompareArrays();
				}

			} else if(currentSlot==0)
							audioManager.GetComponent<AudioManager>().HighBeat();


			else if(currentSlot%subBeatsPerBeat==0)
					audioManager.GetComponent<AudioManager>().LowBeat();

			// SetS the target drum dots
			if(targetDrumArray[currentSlot])
					Instantiate(dotPrefab[0], trail.transform.position, Quaternion.identity);

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
		checkInput=false;
		currentInstrument.GetComponent<Drum>().Cancel();
		GetComponent<LevelManager>().ResetBackground();
	}

	void CompareArrays() {

			if(correctness>0.95 || totalBeats==0)
				ChangeState();
			else{
				currentInstrument.GetComponent<Drum>().Reset();
				if(checkInput)
					GetComponent<LevelManager>().SetGreenBackground();
			}
	}

	void ChangeState() {
			GetComponent<LevelManager>().ResetBackground();
			textField.GetComponent<Text>().text = "Amazing!";
			currentIndex++;
			currentInstrument.transform.localScale= new Vector3 (1,1,1);
			currentInstrument.GetComponent<Drum>().SetActiveness(false);
			currentInstrument.GetComponent<CenterRotation>().enabled=true;
			checkInput=false;

			if((currentIndex < drums.Length)){
				correctness=0;
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

				trail.SetActive(false);
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
