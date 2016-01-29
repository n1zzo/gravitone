using UnityEngine;
using System.Collections;

public class RandomizeNote : MonoBehaviour {

    public int planetNumber = 0;

	// Use this for initialization
	void Start () {
		Randomize();
	}

	public void Randomize(){
		int num=(int) Random.Range(0f,12f);
		GetComponent<ChordPlanet>().baseNote=num + 12*4;
        GetRandomSprite();
	}

    void GetRandomSprite() {
        int num=(int) Random.Range(1f,7f);
        // Load the planet sprite
        this.GetComponent<SpriteRenderer>().sprite = Resources.Load<UnityEngine.Sprite>("Planets/"+num+"/"+planetNumber);
    }

	// Update is called once per frame
	void Update () {

	}
}
