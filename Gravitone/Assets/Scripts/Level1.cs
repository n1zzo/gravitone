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
	public GameObject[] dotPrefab;
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

			if(currentInstrument.GetComponent<Drum>().CheckFire()){

								if ( barNumber==4 || barNumber==3) {

									int slot=currentInstrument.GetComponent<Drum>().UpdateRecord();

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
								} else if ( barNumber>5){

												Cancel();

								} else if ( barNumber==0) {

									barNumber++;
									star.GetComponent<BeatGen>().progress=0.97f;

								}
								checkInput=true;
			}

			currentInstrument.GetComponent<Drum>().widenEffect(correctness);

	}

	// This method is called for each beat
	public override void Beat(int currentSlot) {

			if(barNumber==2){
				int beat=Mathf.RoundToInt(currentSlot/beatsPerBar);
				if(countdown && beat!=0) {
					textField.GetComponent<Text>().text = (beatsPerBar - beat).ToString();
			 	} else if(countdown)
					textField.GetComponent<Text>().text = "Prepare to Tap!";
				if(currentSlot==granularity-1)
					barNumber++;
			}


			if ( currentSlot==0) {

				audioManager.GetComponent<AudioManager>().HighBeat();

				if(barNumber!=0)
					barNumber++;

				if(barNumber==2){
					SetStopPreview();
					countdown=true;
					trail.SetActive(false);
				}

				// Skip 1 and 3 because we will increase the barNumber 2 times
				switch(barNumber){
					case 0:
						trail.SetActive(true);
						trail.GetComponent<Trail>().SetInitialPosition();
						SetPlayPreview();
						textField.GetComponent<Text>().text = "Tap to Play!";
						break;
					case 4:
						trail.SetActive(true);
						trail.GetComponent<Trail>().SetInitialPosition();
						countdown=false;
						textField.GetComponent<Text>().text = "";
						checkInput=false;
						SetRecord();
						break;
					case 5:
						if(checkInput){
							star.GetComponent<BeatGen>().progress=0f;
							SetRecord();
							CompareArrays();
						} else {
							trail.SetActive(false);
							barNumber=2;
							countdown=true;
						}
						break;
				}
			}

			else if(currentSlot%subBeatsPerBeat==0)
					audioManager.GetComponent<AudioManager>().LowBeat();

			if(trail.activeSelf && targetDrumArray[currentSlot])
					Instantiate(dotPrefab[0], trail.transform.position, Quaternion.identity);

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
		destroyDots();
		currentInstrument.GetComponent<Drum>().Cancel();
		trail.SetActive(false);
	}

	private void destroyDots(){
		GameObject[] dots = GameObject.FindGameObjectsWithTag("dot");
		foreach (GameObject dot in dots)
				Destroy(dot);
	}


	void CompareArrays() {
		playerDrumArray=currentInstrument.GetComponent<Drum>().GetDrumArray();

			if(correctness>0.95 || totalBeats==0)
				ChangeState();

	}

	void ChangeState() {

			destroyDots();
			trail.SetActive(false);
			textField.GetComponent<Text>().text = "Amazing!";
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
				GetComponent<LevelManager>().goToNextLevel();

			}

	}

	public void Autocomplete(){
		foreach (GameObject drum in drums)
			drum.GetComponent<Drum>().Autocomplete();
			correctness = 1;
	}

	/*IEnumerator ShowMessage (string message, float delay) {
		 textField.GetComponent<Text>().text = message;
		 textField.SetActive(true);
		 yield return new WaitForSeconds(delay);
		 textField.SetActive(false);
	}*/

}
