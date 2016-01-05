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
	float scaleSpeed;
	float transparencySpeed;
	float transparency;
	CircleCollider2D circleCollider;
	float screenAspect;
	float cameraWidth;
	bool active=false;
	public GameObject Preview;
	public GameObject levelManager;


	// Use this for initialization
	void Start () {
		spriteRenderer = this.GetComponent<SpriteRenderer>();
		star.GetComponent<BeatGen>().Subscribe(this);
		newGranularity = star.GetComponent<BeatGen>().granularity/newGranularityDivision;
		circleCollider = this.GetComponent<CircleCollider2D>();
		scaleSpeed=star.GetComponent<BeatGen>().bpm * 0.2f / 128f;
		transparencySpeed=star.GetComponent<BeatGen>().bpm * 0.2f / 128f;
		transparencySpeed*=0.5f;
		transparency = 1f;
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
			// Ajdust wave opacity
			float transparencyIncrement = (float) transparencySpeed * Time.deltaTime;
			SetTransparency(transparency - transparencyIncrement);
		}
	}

	// This method is called for each beat
	public override void Beat(int currentSlot) {

		if(currentSlot==levelManager.GetComponent<Level2>().GetNumberOfThirdBeat())
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
						// Pass the current ring position to the preview object and create a planet
						Preview.GetComponent<HarmonyPreview>().PlayPlanet(orbitsRadius[currentOrbits]);
						// Increment orbit number
						currentOrbits++;
					}
			}

			if(currentBar==bars){
								levelManager.GetComponent<Level2>().setRadiusPlanets(orbitsRadius);
								star.GetComponent<BeatGen>().Unsubscribe(this);
								gameObject.SetActive(false);
								active=false;
			}

		}
	}

	public void Restart(){
		DestroyPreviews();
		currentOrbits=0;
		currentBar=-1;
		bars=4;
		transform.localScale = new Vector3(0.4f,0.4f,1);
		Preview.GetComponent<HarmonyPreview>().StopPreview();
		star.GetComponent<BeatGen>().Subscribe(this);
	}

	public void DestroyPreviews(){
		GameObject[] previews = Preview.GetComponent<HarmonyPreview>().GetPreviews();
		foreach (GameObject preview in previews)
			Destroy(preview);
	}

	public void SetTransparency(float level){
		transparency = level;
		spriteRenderer.color = new Color(1f,1f,1f,level);
	}

}
