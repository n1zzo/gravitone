using UnityEngine;
using System.Collections;

public class revolution : MonoBehaviour {

	public GameObject star;
	public UnityEngine.UI.Text text1;
	float starX;
	float starY;
	int bpm = 120;
	public int beatsPerBar = 4;
	public float radius = 1f;
	const float TWO_PI = 2*Mathf.PI;
  int beatsPerSlot = 1;
  int totSlots = 4;
  int lastBeat = -1;
  // While progress goes from 0 to 1 we complete one bar
  float progress = 0f;
  float timeSpeed = 0f;
	float currentAngle = 0f;
	float error = 0.02f;
  bool[] slots = new bool[64];
	AudioSource sound;
  bool keyPressed = false;

  // Use this for initialization
	void Start () {

		slots[16] = true;

    totSlots = beatsPerSlot * beatsPerBar;

		// Gets the x and y coordinates and bpm from the reference star
		starX = star.GetComponent<star>().x;
		starY = star.GetComponent<star>().y;
		bpm = star.GetComponent<star>().bpm;

		// Loads the drum clip
		sound = GetComponent<AudioSource>();

		// Sets the initial position
		transform.position = new Vector3(0, radius, 0);

    // Time speed is derived from BPMs
    timeSpeed = (float) bpm / (60 * (float) beatsPerBar);
	}

  // Update is called once per frame
  void Update () {
    // Update the bar's progress
    progress += timeSpeed * Time.deltaTime;

    // Avoid progress overflow
    if (progress >= 1)
      progress -= 1;

    // Calculate the planet's position
    currentAngle = progress * TWO_PI;
		transform.position = getPosition(currentAngle);

    // Records in the array that you pressed a button
    if (!keyPressed && Input.GetKeyDown("space")) {
      Debug.Log("KEK!");
			sound.Play();
      slots[(int) (progress * (float) totSlots)] = true;
      keyPressed = true;
    }
    if (Input.GetKeyUp("space"))
      keyPressed = false;


    /*
		// Plays the sound at every beat (only if it's not playing)
		if (!sound.isPlaying && checkBeat(progress) < error) {
			sound.Play();
		}
    */
		int currentBeat=(int) (progress * (float) totSlots);

		// Plays the sound if the current slot is full
    if (currentBeat!=lastBeat){
      if (checkSlot(currentBeat) )
      	sound.Play();
      lastBeat=currentBeat;
    }
	}

	// Obtains the planet position from the planet's angle
	Vector3 getPosition (float angle) {
		return new Vector3(radius*Mathf.Sin(angle) + starX, radius*Mathf.Cos(angle) + starY, 0);
	}

	// Checks if the current angle corresponds to a beat within a given error range
 	float checkBeat (float progress) {
		return progress % (1 / (float) beatsPerBar);
	}

  bool checkSlot (int currentBeat) {
		text1.text=currentBeat.ToString();
  	return slots[currentBeat];
  }

}
