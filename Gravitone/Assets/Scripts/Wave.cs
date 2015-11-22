using UnityEngine;
using System.Collections;

public class Wave : MonoBehaviour {

	float scaleSpeed = .1f;
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
		circleCollider.radius = size.x / 2;

	}
	void OnTriggerEnter2D(Collider2D other) {
    Debug.Log("A planet has been hit!");
  }

}
