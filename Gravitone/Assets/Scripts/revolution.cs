using UnityEngine;
using System.Collections;

public class revolution : MonoBehaviour {

	public int starX = 0;
	public int starY = 0;
	public int bpm = 60;
	public int beatsPerBar = 4;
	float startTime = 0;

	// Use this for initialization
	void Start () {
		transform.position = new Vector3(0, radius, 0);

	}

	// Update is called once per frame
	void Update () {
		transform.position = getPosition(Time.currentTime)
	}

	Vector3 getPosition (time timePassed) {
		currentPosition = transform.position;
		currentAngle = Mathf.Atan2(currentPosition.y-starY, currentPosition.x-starX);
		nextAngle = currentAngle + angularSpeed * timePassed;
	}

	float AngularSpeed() {
		# BPM are beats per minute, in a circle we have a bar with multiple beats.
		return bpm * beatsPerBar * Mathf.PI	/ 60
	}

}
