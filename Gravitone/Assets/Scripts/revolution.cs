using UnityEngine;
using System.Collections;

public class revolution : MonoBehaviour {

	public GameObject star;
	float starX;
	float starY;
	int bpm = 120;
	public int beatsPerBar = 4;
	public float radius = 1f;
	const float TWO_PI = 2*Mathf.PI;
  const int TOT_SLOTS = 32;
  // While progress goes from 0 to 1 we complete one bar
  float progress = 0f;
  float timeSpeed = 0f;
	float currentAngle = 0f;
	float error = 0.02f;
  bool[] slots = new bool[TOT_SLOTS];
	AudioSource sound;

  // Use this for initialization
	void Start () {

		slots[16] = true;

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

		// The first beat is always missing
		// We trigger it manually at initialization
  	sound.Play();
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

		if (Input.GetKeyDown("space"))
      // Records in the array that you pressed a button
      print("space key was pressed");
      slots[(int) (progress / TOT_SLOTS)] = true;

    /*
		// Plays the sound at every beat (only if it's not playing)
		if (!sound.isPlaying && checkBeat(progress) < error) {
			sound.Play();
		}
    */

    // Plays the sound if the current slot is full
    if (!sound.isPlaying && checkSlot(progress)) {
      sound.Play();
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

  bool checkSlot (float progress) {
  	return slots[(int) (progress * (float) TOT_SLOTS)];
  }

}
