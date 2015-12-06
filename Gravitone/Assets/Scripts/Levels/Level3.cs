using UnityEngine;
using System.Collections;

public class Level3 : MonoBehaviour {

	public GameObject cam;
	public GameObject[] planets;

	// Use this for initialization
	void Start () {

		// Set the camera to follow the first planet.
		cam.GetComponent<SmoothFollow2D>().target = planets[0].transform;
		cam.GetComponent<SmoothFollow2D>().enabled = true;
		cam.GetComponent<SmoothCamera>().setArrival(4f);
		cam.GetComponent<SmoothCamera>().enabled=true;

	}

	// Update is called once per frame
	void Update () {

	}
}
