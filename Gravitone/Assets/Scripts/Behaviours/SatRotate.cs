using UnityEngine;
using System.Collections;

public class SatRotate : MonoBehaviour {

	public GameObject star;
	public float radius;
	private const float TWO_PI = 2*Mathf.PI;
	private float offsetX=0;
	private float offsetY=0;
	private float x;
	private float y;
	public float initialProgress;
	public GameObject planet;
	public bool prev;
	public int index;
	GameObject melody;

	void Start(){
		melody=GameObject.FindWithTag("Melody");
		//ComputeOffset();
	}

	// Update is called once per frame
	void Update () {

		float x = planet.transform.position.x;
		float y = planet.transform.position.y;

    SetOffset(x, y);

		// Gets the current progress from the star
		float progress = star.GetComponent<BeatGen>().progress;

		progress-=initialProgress;

		if(progress<0)
			progress+=1;

		// Calculate the planet's position
		transform.position = getPosition(progress * TWO_PI);
	}

	// Obtains the planet position from the planet's angle
	private Vector3 getPosition (float angle) {
		// Apply initial offset
		angle += Mathf.PI;

		float x = radius*Mathf.Cos(angle);
		float y = radius*Mathf.Sin(angle);
		x += offsetX;
		y += offsetY;
		return new Vector3(x, y, 0);
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

	void OnMouseDown(){
		if(!prev)
			melody.GetComponent<Melodies>().DestroySat(index);
	}

}
