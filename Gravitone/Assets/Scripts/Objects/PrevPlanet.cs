using UnityEngine;
using System.Collections;

public class PrevPlanet : MonoBehaviour {

	public float speed = 0.01f;
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

	void DissolveAndDestroy(){
		Color newColor = GetComponent<Renderer>().material.color;
		newColor.a-=speed;
		if(newColor.a>0)
			GetComponent<Renderer>().material.SetColor("_Color", newColor);
		else
			Destroy(this.gameObject);
	}

	public void Play() {

		if(!audioManager)
			audioManager=GameObject.FindWithTag("AudioManager");
	
		audioManager.GetComponent<AudioManager>().PlayChord(baseNote, chordName);
	}

}
