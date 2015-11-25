using UnityEngine;
using System.Collections;

public class Wave : Subscriber {

	public GameObject star;
	public GameObject[] rings;
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

			if(currentOrbits<rings.Length && orbitSlots[currentIndex] && currentSlot%newGranularity==0){
				orbitsRadius[currentOrbits] = spriteRenderer.bounds.extents.x;
				rings[currentOrbits].SetActive(true);
				rings[currentOrbits].GetComponent<Ring>().SetSize(transform.localScale);
				currentOrbits++;
		}
	}

}
