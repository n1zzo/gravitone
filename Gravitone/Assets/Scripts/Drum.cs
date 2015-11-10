using UnityEngine;
using System.Collections;

public class Drum : Subscriber {

	public GameObject star;
	float scaleStep=0.5f;
	int currentSlot = 0;
	int lastSlot = -1;
	bool lastBeat = false;
	int beatsPerBar = 4;
	public string fireKey="";
	AudioSource sound;
	bool keyPressed = false;

	// Use this for initialization
	void Start () {
		star.GetComponent<BeatGen>().Subscribe(this);

		// Loads the drum clip
		sound = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update () {

		// Gets the current progress from the star
		float progress = star.GetComponent<BeatGen>().progress;

		// Records in the array that you pressed a button
    if (!keyPressed && Input.GetKeyDown(fireKey)) {
			var index=Mathf.RoundToInt(progress * (float) totSlots);

			//If it's divided in N, then the Nth beat is the initial 0
			if(index==totSlots)
				index=0;

			/*The instant sound feedback will be received neither
			  when the slot is already occupied nor when the sound
			  is quantified afterwards (to avoid double sounds)*/
			if(!slots[index] && index == (int)(progress * (float) totSlots)){
				transform.localScale = new Vector3(1, 1, 1);
				sound.Play();
			}

			slots[index] = true;

      keyPressed = true;
    }

    if (Input.GetKeyUp(fireKey))
      keyPressed = false;

		// Plays the sound if the current slot is full, only one time
    if (lastBeat && currentSlot != lastSlot) {
      if (checkSlot(currentSlot)) {
      	sound.Play();
			}
      lastSlot=currentSlot;
    }


		lastBeat = false;
	}

	// This method is called for each beat
	public override void Beat(int currentSlot) {
		this.currentSlot = currentSlot;
		lastBeat = true;
	}

	// Checks if the slot is full
  bool checkSlot (int currentSlot) {
		text1.text=currentSlot.ToString();
  	return slots[currentSlot];
  }

}
