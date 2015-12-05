using UnityEngine;
using System.Collections;

public class Rotate : Subscriber {

	public GameObject star;
	public float radius;
	public bool clockwise = true;
	float starX;
	float starY;
	float offsetAngle;
	int currentBar=0;
	int bars=4;
	const float TWO_PI = 2*Mathf.PI;

	// Use this for initialization
	void Start () {
		// Gets the x and y coordinates and bpm from the reference star
		starX = star.GetComponent<BeatGen>().x;
		starY = star.GetComponent<BeatGen>().y;
		star.GetComponent<BeatGen>().Subscribe(this);
	}

	// Update is called once per frame
	void Update () {
		// Gets the current progress from the star
		float progress = star.GetComponent<BeatGen>().progress;
		// Calculate the progress of the whole circumference
		progress=(progress + currentBar)/bars;
		// Calculate the planet's position
		transform.position = getPosition(progress * TWO_PI);
	}

	// Obtains the planet position from the planet's angle
	private Vector3 getPosition (float angle) {
		// Apply initial offset
		angle += offsetAngle;
		// Consider clockwise option
		if (clockwise)
			angle = -angle;
		float x = radius*Mathf.Cos(angle);
		float y = radius*Mathf.Sin(angle);
		x += starX;
		y += starY;
		return new Vector3(x, y, 0);
	}

	// This method is called for each beat
	public override void Beat(int currentSlot) {
			if(currentSlot==0){
				if(currentBar<bars-1)
					currentBar++;
				else
					currentBar=0;
		 	}
	}

	public void setRadius (float radius){
		this.radius = radius;
	}

	private void OffsetFromPosition(float progress) {
		float x = transform.position.x - starX;
		float y = transform.position.y - starY;
		float relativeAngle = Mathf.Atan2(y, x);
		if(relativeAngle < 0)
			relativeAngle += TWO_PI;
		offsetAngle = relativeAngle - (progress*TWO_PI);
	}

	public void ComputeOffset() {
		float progress = star.GetComponent<BeatGen>().progress;
		// Calculate the progress of the whole circumference
		progress=(progress + currentBar)/bars;
		OffsetFromPosition(progress);
	}

}
