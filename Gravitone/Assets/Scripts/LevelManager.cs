using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public GameObject kick;
	public GameObject snare;
	public GameObject hat;
	GameObject currentInstrument;

	// Use this for initialization
	void Start () {
		currentInstrument=kick;
	}

	// Update is called once per frame
	void Update () {

	}

	public void SetRecord () {
		currentInstrument.GetComponent<Drum>().ToggleRecord();
	}

	public void SetPlayPreview () {
		currentInstrument.GetComponent<Drum>().PlayPreview();
	}

	public void SetStopPreview () {
		currentInstrument.GetComponent<Drum>().StopPreview();
	}

	public void Cancel(){
		currentInstrument.GetComponent<Drum>().Cancel();
	}

}
