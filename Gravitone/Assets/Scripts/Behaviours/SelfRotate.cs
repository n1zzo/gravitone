using UnityEngine;
using System.Collections;

public class SelfRotate : MonoBehaviour {

	public bool backward;
	float speed=1f;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if(!backward)
			transform.Rotate (Vector3.forward * speed);
		else
			transform.Rotate (Vector3.forward * speed);
	}
}
