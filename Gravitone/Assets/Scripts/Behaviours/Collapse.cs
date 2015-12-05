using UnityEngine;
using System;
using System.Collections;

public class Collapse : MonoBehaviour {

	public float speed = 1f;
	public float acceleration = 1f;
	private Action restore;

	void Start () {
		GetComponent<ChordPlanet>().active=false;
	}

	// Update is called once per frame
	void Update () {
		float radius = GetComponent<Rotate>().radius;
		if (radius <= 0) {
			EndCollapse();
		}
		GetComponent<Rotate>().radius = radius - speed*Time.deltaTime;
		speed += acceleration;
	}

	private void EndCollapse () {
		GetComponent<Collapse>().enabled=false;
		restore();
	}

	public void SetRestore(Action restore) {
		this.restore = restore;
	}
}
