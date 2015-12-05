using UnityEngine;
using System;
using System.Collections;

public class Collapse : MonoBehaviour {

	float speed = 0.1f;
	float acceleration = 0.2f;
	private Action restore;

	void Start () {
		GetComponent<ChordPlanet>().active=false;
		GetComponent<SpriteRenderer>().sortingOrder=1;
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
