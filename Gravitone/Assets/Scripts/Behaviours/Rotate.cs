using UnityEngine;
using System.Collections;

public class Rotate : Subscriber {

	public GameObject star;
	public float radius;
	public bool clockwise;
	private float offsetAngle;
	private int currentBar=0;
	private int bars=4;
	private const float TWO_PI = 2*Mathf.PI;
	private bool dirtyOffset = true;
	private float offsetX;
	private float offsetY;

	// Use this for initialization
	void Start () {
		// Gets the x and y coordinates and bpm from the reference star
		star.GetComponent<BeatGen>().Subscribe(this);
	}

	// Update is called once per frame
	void Update () {
		if (dirtyOffset) {
			ComputeOffset();
			dirtyOffset = false;
		}
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
		x += offsetX;
		y += offsetY;
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

	public void SetRadius (float radius){
		this.radius = radius;
		Update();
	}

	private void OffsetFromPosition(float progress) {
		float x = transform.position.x + offsetX;
		float y = transform.position.y + offsetY;
		float relativeAngle = Mathf.Atan2(y, x);
		if(relativeAngle < 0)
			relativeAngle += TWO_PI;
		offsetAngle = relativeAngle - (progress*TWO_PI);
	}

	private void ComputeOffset() {
		float progress = star.GetComponent<BeatGen>().progress;
		// Calculate the progress of the whole circumference
		progress=(progress + currentBar)/bars;
		OffsetFromPosition(progress);
	}

	public void SetDirtyOffset() {
		dirtyOffset = true;
	}

	public void SetOffset(float x, float y) {
		this.offsetX = x;
		this.offsetY = y;
	}

}
