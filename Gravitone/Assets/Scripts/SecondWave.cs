using UnityEngine;
using System.Collections;

public class SecondWave : Subscriber {


		public float scaleSpeed = .1f;
		CircleCollider2D circleCollider;
		SpriteRenderer spriteRenderer;

		// Use this for initialization
		void Start () {
			circleCollider = this.GetComponent<CircleCollider2D>();
			spriteRenderer = this.GetComponent<SpriteRenderer>();
		}

		// Update is called once per frame
		void Update () {
			float scaleIncrement = (float) scaleSpeed * Time.deltaTime;
			// Incrementally adapts to the target scale
			transform.localScale += new Vector3(scaleIncrement, scaleIncrement, 0);
			Vector3 size = spriteRenderer.bounds.size;
			float radius = size.x * transform.localScale.x;
			circleCollider.radius = (radius / 100f) + 40;

		}

}
