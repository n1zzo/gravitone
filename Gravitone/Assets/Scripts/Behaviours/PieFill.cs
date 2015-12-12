using UnityEngine;
using System.Collections;

public class PieFill : Subscriber {

	public GameObject[] pie;
	public GameObject star;

	// Use this for initialization
	void Start () {
		pie=GameObject.FindGameObjectsWithTag("Pie");

		foreach(GameObject piece in pie)
			piece.SetActive(false);

		star.GetComponent<BeatGen>().Subscribe(this);
	}

	// Update is called once per frame
	void Update () {

	}

	public override void Beat(int currentSlot){
		int currentPie=currentSlot%16;
		int lastPie;
		if(currentPie==0)
			lastPie=15;
		else
			lastPie=currentPie-1;

		pie[currentPie].SetActive(true);
		pie[lastPie].SetActive(false);
	}

}
