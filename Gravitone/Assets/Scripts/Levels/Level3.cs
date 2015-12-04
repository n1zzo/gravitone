using UnityEngine;
using System.Collections;

public class Level3 : MonoBehaviour {

	public GameObject cam;
	public GameObject[] planets;

	// Use this for initialization
	void Start () {

		// Set the camera to follow the first planet.
		cam.GetComponent<SmoothFollow>().target = planets[0].transform;
		cam.GetComponent<SmoothFollow>().enabled = true;

	}

	// Update is called once per frame
	void Update () {

	}
}
