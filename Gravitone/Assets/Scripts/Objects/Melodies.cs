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
	private int totalNotes;
	private int granularity;
	public GameObject[] planets;
	private int currentPlanet;

	// Use this for initialization
	void Start () {
		totalNotes=0;

		// Subscribe to the star
		star.GetComponent<BeatGen>().Subscribe(this);

		granularity=star.GetComponent<BeatGen>().granularity;

		calculateTotalNotes();

		currentPlanet=0;
	}

	// Update is called once per frame
	void Update () {

	}

	// currentSlot ranges from 0 to 15
	public override void Beat(int currentSlot){
		if(currentSlot==0)
			planets[currentPlanet].GetComponent<ChordPlanet>().Play();

		this.currentSlot = currentSlot;
		// Find current index of array
		index = currentSlot+currentBar*granularity;
		// Play the user's saved note
		int noteToPlay = playerNotes[index];
		if(noteToPlay != 0)
			audioManager.GetComponent<AudioManager>().PlayStrings(noteToPlay);
	}

	public void RecordNote(int note) {
		playerNotes[index] = note;
		audioManager.GetComponent<AudioManager>().PlayStrings(note);
		Verify();
	}

	private void Verify() {
		int matching = 0;
		for (int i=currentBar*granularity ; i<(granularity + currentBar*granularity); i++)
				if(melodyNotes[i]!=0 && melodyNotes[i]==playerNotes[i])
					matching++;

		correctness = matching / totalNotes;

		if(correctness==1)
			NextBar();
	}

	public void NextBar() {
		currentBar++;
		calculateTotalNotes();
		currentPlanet++;
	}

	public void calculateTotalNotes(){
		for (int i=currentBar*granularity ; i<granularity +currentBar*granularity ; i++){
			playerNotes[i]=0;
			if(melodyNotes[i]!=0)
				totalNotes++;
		}
	}

	public GameObject GetCurrentPlanet(){
		return planets[currentPlanet];
	}

}
