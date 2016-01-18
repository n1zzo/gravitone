using UnityEngine;
using System.Collections;

public class Globals : MonoBehaviour {

    public int number = 0;
    public int bpm=128;
    public int beatsPerBar=4;
    public int subBeatsPerBeat=4;

	// Use this for initialization
	void Start () {
        // Make this object persistent across level loadings
        DontDestroyOnLoad(this);

	}

	// Update is called once per frame
	void Update () {

	}

    public void SetLevelNumber(int number) {
        this.number = number;
    }

    public int GetLevelNumber() {
        return number;
    }
    
}
