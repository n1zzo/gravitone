using UnityEngine;
using System.Collections;

public class Wave : MonoBehaviour {

	float radius = 0f;
	float scaleSpeed = .005f;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		transform.localScale += new Vector3(scaleSpeed, scaleSpeed, 0);
	}
}
