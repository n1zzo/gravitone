using UnityEngine;
using System.Collections;

public class Drum : Subscriber {

	public GameObject star;
	float scaleStep=0.5f;
	int currentSlot = 0;
	int lastSlot = -1;
	bool lastBeat = false;
	int beatsPerBar = 0;
	int subBeatsPerBeat = 0;
	int granularity = 0;
	public string fireKey="";
	public bool isRecord=false;
	AudioSource sound;
	bool[] slots = new bool[64];

	// is true if we consider the right part of the screen for the touch
	public bool isRight=false;

	// Use this for initialization
	void Start () {
		star.GetComponent<BeatGen>().Subscribe(this);
		beatsPerBar = star.GetComponent<BeatGen>().beatsPerBar;
		subBeatsPerBeat = star.GetComponent<BeatGen>().subBeatsPerBeat;

		granularity = beatsPerBar * subBeatsPerBeat;

		// Loads the drum clip
		sound = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update () {
//
//		if(transform.localScale.x>0.75){
//			float scaleFactor = scaleStep * Time.deltaTime;
//			transform.localScale -= new Vector3(scaleFactor, scaleFactor, 0);
//		}

		// Gets the current progress from the star
		float progress = star.GetComponent<BeatGen>().progress;

		// Records in the array that you pressed a button
	    if (checkFire()) {
				// If he is recording the Rhythm will be memorized
				if (isRecord) {
					var index = Mathf.RoundToInt(progress * (float) granularity);

					// If it's divided in N, then the Nth beat is the initial 0
					if(index == granularity)
						index = 0;

					/*The instant sound feedback will be received neither
					  when the slot is already occupied nor when the sound
					  is quantified afterwards (to avoid double sounds)*/
					if(!slots[index] && index == (int)(progress * (float) granularity)) {
						transform.localScale = new Vector3(1, 1, 1);
						playDrum();
					}

					slots[index] = true;

		    }	else
				// Instead , the sound is played
						playDrum();

			}

			// This is executed at every beat.
		  if (lastBeat && currentSlot != lastSlot) {
		    	if (slots[currentSlot]) {
		      	playDrum();
					}
		      lastSlot=currentSlot;
					lastBeat = false;
			}

	}

	// This method is called for each beat
	public override void Beat(int currentSlot) {
		this.currentSlot = currentSlot;
		lastBeat = true;
	}

	public void changeRecord(){
		isRecord=!isRecord;
	}

	// check if the user touches the right position
	private bool checkPosition(Vector2 pos){
		if(!isRight)
			return pos.x<Screen.width/2;
		else
			return pos.x>Screen.width/2;
	}

	private bool checkFire(){
			//check if our current system info equals a desktop
		 if(SystemInfo.deviceType == DeviceType.Desktop){
		     //we are on a desktop device, so don't use touch
		     return Input.GetKeyDown(fireKey);
		 }
		 //if it isn't a desktop, lets see if our device is a handheld device aka a mobile device
		 else if(SystemInfo.deviceType == DeviceType.Handheld){
		     //we are on a mobile device, so lets use touch input
				 bool checkInput=false;
			 		for (int i = 0; i < Input.touchCount; ++i)
			 			if(Input.GetTouch(i).phase == TouchPhase.Began &&
			 					checkPosition(Input.GetTouch(i).position))
								checkInput=true;
					return checkInput;
		 }
		 else
		 	return false;
	}

	private void playDrum(){
		sound.Play();
		transform.localScale = new Vector3(1, 1, 1);
	}

	public void cancel(){
		// This erases the recorded Rhythm
			for(int i=0; i<slots.Length; i++)
						slots[i]=false;
	}

	// Get a deep copy of the slots array
		public bool[] getDrumArray() {
		bool[] copy = new bool[64];
		System.Array.Copy(slots, copy, 64);
		return copy;
	}

}
