using UnityEngine;
using System.Collections;

public class PrevPlanet : MonoBehaviour {

	public int position=-1;
	public string chordName;
	public int baseNote;
	GameObject audioManager;


	// Use this for initialization
	void Start () {
		audioManager=GameObject.FindWithTag("AudioManager");
	}

	// Update is called once per frame
	void Update () {

	}

	void OnMouseDown () {
		Play();
	}

	public void Play() {

		if(!audioManager)
			audioManager=GameObject.FindWithTag("AudioManager");

		audioManager.GetComponent<AudioManager>().PlayChord(baseNote, chordName);
	}

}
