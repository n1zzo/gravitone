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
	public string recordKey="";
	public string cancelKey="";
	AudioSource sound;
	bool[] slots = new bool[64];

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

		// Gets the current progress from the star
		float progress = star.GetComponent<BeatGen>().progress;

		// Records in the array that you pressed a button
    if (Input.GetKeyDown(fireKey)) {
			// If he is recording the Rhythm will be memorized
			if (isRecord()) {
				var index = Mathf.RoundToInt(progress * (float) granularity);

				// If it's divided in N, then the Nth beat is the initial 0
				if(index == granularity)
					index = 0;

				/*The instant sound feedback will be received neither
				  when the slot is already occupied nor when the sound
				  is quantified afterwards (to avoid double sounds)*/
				if(!slots[index] && index == (int)(progress * (float) granularity)) {
					transform.localScale = new Vector3(1, 1, 1);
					sound.Play();
				}

				slots[index] = true;

	    }	else
			// Instead , the sound is played
					sound.Play();

		}

		// This is executed at every beat.
	  if (lastBeat && currentSlot != lastSlot) {
	    	if (slots[currentSlot]) {
	      	sound.Play();
				}
	      lastSlot=currentSlot;
				lastBeat = false;
		}

		// This erases the recorded Rhythm
		if(Input.GetKeyDown(cancelKey))
			for(int i=0; i<slots.Length; i++)
						slots[i]=false;
	}

	// This method is called for each beat
	public override void Beat(int currentSlot) {
		this.currentSlot = currentSlot;
		lastBeat = true;
	}

	public bool isRecord(){
		return  Input.GetKey(recordKey);
	}

}
