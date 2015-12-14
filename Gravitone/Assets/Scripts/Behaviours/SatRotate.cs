using UnityEngine;
using System.Collections;

public class SatRotate : MonoBehaviour {

	public GameObject star;
	public float radius;
	public bool clockwise;
	private const float TWO_PI = 2*Mathf.PI;
	private float offsetX;
	private float offsetY;
	private float x;
	private float y;
	private float offsetAngle;
	public GameObject planet;

	void Start(){
		ComputeOffset();
	}

	// Update is called once per frame
	void Update () {
		float x = planet.transform.position.x;
		float y = planet.transform.position.y;

    SetOffset(x, y);
		
		// Gets the current progress from the star
		float progress = star.GetComponent<BeatGen>().progress;
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

	private void OffsetFromPosition(float progress) {
		float x = transform.position.x + offsetX;
		float y = transform.position.y + offsetY;
		float relativeAngle = Mathf.Atan2(y, x);
		if(relativeAngle < 0)
			relativeAngle += TWO_PI;
		offsetAngle = relativeAngle - (progress*TWO_PI);
	}

	public void ComputeOffset() {
		float progress = star.GetComponent<BeatGen>().progress;
		OffsetFromPosition(progress);
	}

	public void SetRadius (float radius){
		this.radius = radius;
		Update();
	}

	public void SetOffset(float x, float y) {
		this.offsetX = x;
		this.offsetY = y;
		// Set satellite initial position
		x = offsetX + 4f;
		y = offsetY;
	}

}
