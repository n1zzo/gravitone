﻿using UnityEngine;
using System.Collections;

public class SecondWave : Subscriber {


		float scaleSpeed;
		CircleCollider2D circleCollider;
		SpriteRenderer spriteRenderer;
		float screenAspect;
    float cameraWidth;
    float offset = 3f;

		// Use this for initialization
		void Start () {
			scaleSpeed=GameObject.FindWithTag("MainStar").GetComponent<BeatGen>().bpm * 0.2f / 128f;
			circleCollider = this.GetComponent<CircleCollider2D>();
			spriteRenderer = this.GetComponent<SpriteRenderer>();
			screenAspect = (float)Screen.width / (float)Screen.height;
	    cameraWidth = Camera.main.orthographicSize * screenAspect;
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
		}

}
