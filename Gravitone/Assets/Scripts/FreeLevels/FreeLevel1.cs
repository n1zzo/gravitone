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

	float[][] colors=new float[3][];

	int currentColor=1;


	// Use this for initialization
	void Start () {

		star.GetComponent<BeatGen>().Subscribe(this);

		fillColors();

		canvas.transform.GetChild(1).gameObject.GetComponent<CanvasRenderer>().SetColor(
			new Color(colors[currentColor][0],colors[currentColor][1],colors[currentColor][2],1));

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

	void fillColors(){
			colors[0]=new float[3];
			colors[0][0]=255;
			colors[0][1]=255;
			colors[0][2]=255;
			colors[1]=new float[3];
			colors[1][0]=1;
			colors[1][1]=0.663f;
			colors[1][2]=0.663f;
			colors[2]=new float[3];
			colors[2][0]=0;
			colors[2][1]=1;
			colors[2][2]=0.835f;
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
				currentColor++;
				if(currentColor<colors.Length)
					canvas.transform.GetChild(1).gameObject.GetComponent<CanvasRenderer>().SetColor(
						new Color(colors[currentColor][0],colors[currentColor][1],colors[currentColor][2],1));
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
