using UnityEngine;
using System.Collections;

public class Rotate : Subscriber {

	public GameObject star;
	public float radius = 1f;
	float starX;
	float starY;


	// Use this for initialization
	void Start () {

		// Gets the x and y coordinates and bpm from the reference star
		starX = star.GetComponent<star>().x;
		starY = star.GetComponent<star>().y;

		// Sets the initial position
		if(initialUp)
			transform.position = new Vector3(0, radius, 0);

	}

	// Update is called once per frame
	void Update () {

		// Calculate the planet's position
    currentAngle = progress * TWO_PI;
		transform.position = getPosition(currentAngle);

	}

	// This method is called for each beat
	void Beat() {

	}

	// Obtains the planet position from the planet's angle
	Vector3 getPosition (float angle) {
		if(initialUp)
			return new Vector3(radius*Mathf.Sin(angle) + starX, radius*Mathf.Cos(angle) + starY, 0);
		else
			return new Vector3(radius*Mathf.Sin(-angle) + starX, radius*Mathf.Cos(-angle) + starY, 0);
	}

}
