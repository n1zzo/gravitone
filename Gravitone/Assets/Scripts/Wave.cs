using UnityEngine;
using System.Collections;

public class Wave : Subscriber {

	float scale = 0.005f;
	public float scaleStep = 0.05f;
	float scaleIncrement = 0.0018f;
	public GameObject star;
	int subBeatsPerBeat = 4;
	int updates = 0;
	int updatesPerBeat = 28;
	CircleCollider2D circleCollider;

	// Use this for initialization
	void Start () {
		subBeatsPerBeat = star.GetComponent<BeatGen>().subBeatsPerBeat;
		star.GetComponent<BeatGen>().Subscribe(this);
		circleCollider = this.GetComponent<CircleCollider2D>();
	}

	// Update is called once per frame
	void Update () {
		updates++;
		// Incrementally adapts to the target scale
		transform.localScale += new Vector3(scaleIncrement, scaleIncrement, 0);
		}

	// This method is called for each beat
	public override void Beat(int currentSlot) {
		// At every new bar the scale is increased
		if(currentSlot%subBeatsPerBeat == 0) {
			// Update the number of updates per bar
			updatesPerBeat = updates;
			updates = 0;
			// Increment the target scale
			scale += scaleStep;
			float actualScale = transform.localScale.x;
			// Calculates how much the scale must be incremented at each update
			scaleIncrement = (scale - actualScale) / (float) updatesPerBeat;
			Debug.Log(updatesPerBeat);
		}
	}
}
