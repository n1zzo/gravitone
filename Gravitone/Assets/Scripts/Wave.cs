using UnityEngine;
using System.Collections;

public class Wave : MonoBehaviour {

	float scale = 0.005f;
	float scaleSpeed = .1f;
	CircleCollider2D circleCollider;

	// Use this for initialization
	void Start () {
		circleCollider = this.GetComponent<CircleCollider2D>();
	}

	// Update is called once per frame
	void Update () {
		float scaleIncrement = (float) scaleSpeed * Time.deltaTime;
		// Incrementally adapts to the target scale
		transform.localScale += new Vector3(scaleIncrement, scaleIncrement, 0);
		Debug.Log(scaleIncrement);
	}

}
