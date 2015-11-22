using UnityEngine;
using System.Collections;

public class Wave : Subscriber {

	public float scaleSpeed = .1f;
	public GameObject star;
	public GameObject[] planets;
	CircleCollider2D circleCollider;
	SpriteRenderer spriteRenderer;
	public float[] orbitsRadius;
	public bool[] orbitSlots;
	int currentOrbits=0;
	int currentBar=0;
	public int bars=4;
	public int newGranularity;

	// Use this for initialization
	void Start () {
		transform.localScale= new Vector3(0.1f,0.1f,0.1f);
		circleCollider = this.GetComponent<CircleCollider2D>();
		spriteRenderer = this.GetComponent<SpriteRenderer>();
		star.GetComponent<BeatGen>().Subscribe(this);
		newGranularity=star.GetComponent<BeatGen>().granularity/2;
	}

	// Update is called once per frame
	void Update () {
		float scaleIncrement = (float) scaleSpeed * Time.deltaTime;
		// Incrementally adapts to the target scale
		transform.localScale += new Vector3(scaleIncrement, scaleIncrement, 0);
		Vector3 size = spriteRenderer.bounds.size;
		float radius = size.x * transform.localScale.x;
		circleCollider.radius = (radius / 100f) + 40;

		bool trigger=star.GetComponent<BeatGen>().endTrigger;

		if(trigger && currentBar<bars-1)
			currentBar++;
		else if (trigger) {
			Destroy(this);
		}


	}

	// This method is called for each beat
	public override void Beat(int currentSlot) {

			int currentIndex=Mathf.CeilToInt(currentSlot/2) + (newGranularity*currentBar);
			if(orbitSlots[currentIndex] && currentSlot%newGranularity==0 && currentOrbits<planets.Length){
				orbitsRadius[currentOrbits] = spriteRenderer.bounds.extents.x;
				planets[currentOrbits].SetActive(true);
				currentOrbits++;

		}
	}


}
