using UnityEngine;
using System.Collections;

public class Melodies : Subscriber {

	public GameObject star;
	private int[] melodyNotes = new int[64];
	private int[] playerNotes = new int[64];
	private int currentSlot;
	public GameObject audioManager;

	// Use this for initialization
	void Start () {
		// Subscribe to the star
		star.GetComponent<BeatGen>().Subscribe(this);
	}

	// Update is called once per frame
	void Update () {

	}

	// currentSlot ranges from 0 to 15
	public override void Beat(int currentSlot){
		this.currentSlot = currentSlot;
		// Play the user's saved note
		int noteToPlay = playerNotes[currentSlot];
		if(noteToPlay != -1)
			audioManager.GetComponent<AudioManager>().PlayStrings(noteToPlay);
	}

	public void RecordNote(int note) {
		playerNotes[currentSlot] = note;
		Verify();
	}

	private void Verify() {

	}

}
