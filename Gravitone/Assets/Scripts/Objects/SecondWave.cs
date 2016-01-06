using UnityEngine;
using System.Collections;

public class SecondWave : Subscriber {

		private GameObject star;
		float scaleSpeed;
		CircleCollider2D circleCollider;
		SpriteRenderer spriteRenderer;
		float screenAspect;
    float cameraWidth;
    float offset = 3f;
		float transparencySpeed;
		float transparency;

		// Use this for initialization
		void Start () {
			star = GameObject.FindGameObjectsWithTag("MainStar")[0];
			// The first float is half of the scale of the wave, 128 is our standard bpm.
			scaleSpeed=GameObject.FindWithTag("MainStar").GetComponent<BeatGen>().bpm * 0.0757f / 128f;
			circleCollider = this.GetComponent<CircleCollider2D>();
			spriteRenderer = this.GetComponent<SpriteRenderer>();
			screenAspect = (float)Screen.width / (float)Screen.height;
	    cameraWidth = Camera.main.orthographicSize * screenAspect;
			transparencySpeed=star.GetComponent<BeatGen>().bpm * 0.15f / 128f;
			transparencySpeed*=0.5f;
			transparency = 1f;
		}

		// Update is called once per frame
		void Update () {
			float scaleIncrement = (float) scaleSpeed * Time.deltaTime;
			// Incrementally adapts to the target scale
			transform.localScale += new Vector3(scaleIncrement, scaleIncrement, 0);
			Vector3 size = spriteRenderer.bounds.extents;
			float radius = size.x * transform.localScale.x;
			circleCollider.radius = (radius / 100f) + 7.4f;

			if(GetComponent<SpriteRenderer>().bounds.extents.x>cameraWidth+offset)
							Destroy(this.gameObject);
							// Ajdust wave opacity
			float transparencyIncrement = (float) transparencySpeed * Time.deltaTime;
			SetTransparency(transparency - transparencyIncrement);
		}

		public void SetTransparency(float level){
			transparency = level;
			spriteRenderer.color = new Color(1f,1f,1f,level);
		}

}
