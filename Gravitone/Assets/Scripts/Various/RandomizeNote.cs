using UnityEngine;
using System.Collections;

public class RandomizeNote : MonoBehaviour {

	// Use this for initialization
	void Start () {
		int num=(int) Random.Range(0f,12f);
		GetComponent<ChordPlanet>().baseNote=num + 12*4;
	}

	// Update is called once per frame
	void Update () {

	}
}
