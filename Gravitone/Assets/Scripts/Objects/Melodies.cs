using UnityEngine;
using System.Collections;

public class Melodies : Subscriber {

	public GameObject star;
	public GameObject audioManager;
	public int[] melodyNotes = new int[64];
	private int[] playerNotes = new int[64];
	private int currentSlot;
	private int currentBar;
	private int index;
	private float correctness = 0;

	// Use this for initialization
	void Start () {
		// Subscribe to the star
		star.GetComponent<BeatGen>().Subscribe(this);

		for (int i=0 ; i<melodyNotes.Length ; i++){
			melodyNotes[i]=-1;
			playerNotes[i]=-1;
		}
	}

	// Update is called once per frame
	void Update () {

	}

	// currentSlot ranges from 0 to 15
	public override void Beat(int currentSlot){
		this.currentSlot = currentSlot;
		// Find current index of array
		index = currentSlot+currentBar*16;
		// Play the user's saved note
		int noteToPlay = playerNotes[index];
		if(noteToPlay != -1)
			audioManager.GetComponent<AudioManager>().PlayStrings(noteToPlay);
	}

	public void RecordNote(int note) {
		playerNotes[index] = note;
		Verify();
	}

	private void Verify() {
		int totalNotes = 0;
		int matching = 0;
		for(int i = 0; i < 64; i++) {
			if(melodyNotes[i] != -1) {
				totalNotes++;
				if(melodyNotes[i]==playerNotes[i])
					matching++;
			}
		}
		correctness = matching/totalNotes;
	}

	public void NextBar() {
		currentBar++;
	}

}
