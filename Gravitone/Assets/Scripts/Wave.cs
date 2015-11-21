using UnityEngine;
using System.Collections;

public class Wave : Subscriber {

	float radius = 0f;
	float scaleSpeed = 1f;
	public GameObject star;
	int subBeatsPerBeat = 4;

	// Use this for initialization
	void Start () {
		subBeatsPerBeat = star.GetComponent<BeatGen>().subBeatsPerBeat;
		star.GetComponent<BeatGen>().Subscribe(this);
	}

	// Update is called once per frame
	void Update () {
		//transform.localScale += new Vector3(scaleSpeed, scaleSpeed, 0);
	}

	// This method is called for each beat
	public virtual void Beat(int currentSlot) {
		// check every bar if the array is correct
		if(currentSlot%subBeatsPerBeat == 0)
			Debug.Log("LOL");
			transform.localScale += new Vector3(scaleSpeed, scaleSpeed, 0);
	}
}
