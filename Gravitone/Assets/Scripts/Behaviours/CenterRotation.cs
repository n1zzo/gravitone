using UnityEngine;
using System.Collections;

public class CenterRotation : Subscriber {

	public GameObject star;
	public float radius = 1f;
	public bool clockwise = true;
	const float TWO_PI = 2*Mathf.PI;
	public float angularSpeed = 0f;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {

		transform.Rotate(0, 0, angularSpeed);

	}

	// This method is called for each beat
	public override void Beat(int currentSlot) {

	}

}
