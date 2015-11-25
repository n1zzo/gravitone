using UnityEngine;
using System.Collections;

public class Drag : MonoBehaviour {

	private Vector3 screenPoint;
	private Vector3 offset;
	private Collider2D lastCollision;
	private bool active = true;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	void OnMouseDown () {
		if(active) {
			screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
			offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
		}
	}

	void OnMouseDrag () {
		if(active) {
			Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
			Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
			transform.position = cursorPosition;
		}
	}

	void OnMouseUp () {
		if(active) {
			// Assign the planet to the nearest orbit.
			Debug.Log(lastCollision.gameObject);
			// Get the radius from the orbit and give it to Rotate.cs
			// Deactivate the drag functionality
			active = false;
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(active) {
			lastCollision = other;
		}
  }

}
