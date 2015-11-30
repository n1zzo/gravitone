using UnityEngine;
using System.Collections;

public class Wave : Subscriber {

	public GameObject star;
	public GameObject[] rings;
	SpriteRenderer spriteRenderer;
	public float[] orbitsRadius;
	public bool[] orbitSlots;
	int currentOrbits=0;
	int currentBar=-1;
	public int bars=4;
	public int newGranularityDivision=2;
	int newGranularity;
	public float scaleSpeed=0.1f;
	CircleCollider2D circleCollider;
	float screenAspect;
	float cameraWidth;
	bool active=false;
	public GameObject Preview;

	// Use this for initialization
	void Start () {
		spriteRenderer = this.GetComponent<SpriteRenderer>();
		star.GetComponent<BeatGen>().Subscribe(this);
		newGranularity = star.GetComponent<BeatGen>().granularity/newGranularityDivision;
		circleCollider = this.GetComponent<CircleCollider2D>();
	}

	// Update is called once per frame
	void Update () {
		if(active){
			float scaleIncrement = (float) scaleSpeed * Time.deltaTime;
			// Incrementally adapts to the target scale
			transform.localScale += new Vector3(scaleIncrement, scaleIncrement, 0);
			Vector3 size = spriteRenderer.bounds.extents;
			float radius = size.x * transform.localScale.x;
			circleCollider.radius = (radius / 100f) + 7.4f;
		}
	}

	// This method is called for each beat
	public override void Beat(int currentSlot) {

		if(currentSlot==star.GetComponent<BeatGen>().granularity-star.GetComponent<BeatGen>().subBeatsPerBeat)
						active=true;

		if(active){
			if(currentSlot==0)
							currentBar++;

			if(currentBar!=-1 && currentBar < bars){
					int currentIndex=Mathf.CeilToInt(currentSlot/newGranularityDivision) + (newGranularity*currentBar);

					if(currentOrbits<rings.Length && orbitSlots[currentIndex] && currentSlot%newGranularity==0){
						orbitsRadius[currentOrbits] = spriteRenderer.bounds.extents.x;
						rings[currentOrbits].SetActive(true);
						rings[currentOrbits].GetComponent<Ring>().SetSize(transform.localScale);
						currentOrbits++;
					}
			}

			if(currentBar==bars){
								star.GetComponent<BeatGen>().Unsubscribe(this);
								// Pass the rings positions to the preview object and triggers the preview.
								Preview.GetComponent<HarmonyPreview>().orbitsRadius = orbitsRadius;
								Preview.GetComponent<HarmonyPreview>().StartPreview();
								Destroy(this.gameObject);
			}
		}
	}
}
