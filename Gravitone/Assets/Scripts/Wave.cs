using UnityEngine;
using System.Collections;

public class Wave : Subscriber {

	public float scaleSpeed;
	public GameObject star;
	public GameObject[] planets;
	SpriteRenderer spriteRenderer;
	public float[] orbitsRadius;
	public bool[] orbitSlots;
	int currentOrbits=0;
	int currentBar=0;
	public int bars=4;
	public int newGranularityDivision=2;
	int newGranularity;

	// Use this for initialization
	void Start () {
		spriteRenderer = this.GetComponent<SpriteRenderer>();
		star.GetComponent<BeatGen>().Subscribe(this);
		newGranularity = star.GetComponent<BeatGen>().granularity/newGranularityDivision;
	}

	// Update is called once per frame
	void Update () {

	}

	// This method is called for each beat
	public override void Beat(int currentSlot) {
			if(currentSlot==0){
				if(currentBar<bars-1)
					currentBar++;
				else{
					GetComponent<Wave>().enabled=false;
				}
			}

			int currentIndex=Mathf.CeilToInt(currentSlot/newGranularityDivision) + (newGranularity*currentBar);

			if(orbitSlots[currentIndex] && currentSlot%newGranularity==0 && currentOrbits<planets.Length){
				orbitsRadius[currentOrbits] = spriteRenderer.bounds.extents.x;
				planets[currentOrbits].SetActive(true);
				currentOrbits++;
		}
	}

}
