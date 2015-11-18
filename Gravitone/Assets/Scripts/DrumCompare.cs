using UnityEngine;
using System.Collections;

public class DrumCompare : MonoBehaviour {

	bool[] playerDrumArray;
	public bool[] targetDrumArray = new bool[64];
	float correctness = 0;

	// Use this for initialization
	void Start () {
		updatePlayerArray();
	}

	// Update is called once per frame
	void Update () {

	}

	void updatePlayerArray() {
		playerDrumArray = this.GetComponent<Drum>().GetDrumArray();
	}

	void compareArrays() {
		int totalBeats = 0;
		int hit = 0;
		int wrong = 0;
		// This could be optimized by checking only the used cells.
		for(int i=0; i<64; i++) {
			if(targetDrumArray[i]) {
				totalBeats++;
				if(playerDrumArray[i])
					hit++;
			} else if(playerDrumArray[i])
				wrong++;
			int balance = hit - wrong;
			if(balance > 0)
				correctness = (float)balance/(float)totalBeats;
			else
				correctness = 0;
		}
	}

	public bool[] get(){
		return targetDrumArray;
	}
}
