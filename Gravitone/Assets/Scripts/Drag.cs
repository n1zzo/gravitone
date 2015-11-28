using UnityEngine;
using System.Collections;

public class Drag : MonoBehaviour {

	private Vector3 screenPoint;
	private Vector3 offset;
	private Collider2D lastCollision;
	public float afterColliderRadius=0.1f;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	void OnMouseDown () {

			screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
			offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

	}

	void OnMouseDrag () {

			Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
			Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
			transform.position = cursorPosition;

	}

	void OnMouseUp () {

			// Assign the planet to the nearest orbit.
			// Get the radius from the orbit and give it to Rotate.cs
			// Deactivate the drag functionality
			if(lastCollision){

					GetComponent<Rotate>().enabled=true;
					GetComponent<Rotate>().setRadius(lastCollision.gameObject.GetComponent<Ring>().GetRadius());
					lastCollision.gameObject.GetComponent<Ring>().SetItHas();

					// We can adjust this to avoid the CHORD DELAY !!!!
					GetComponent<CircleCollider2D>().radius=afterColliderRadius;

					GetComponent<ChordPlanet>().active=true;

					GetComponent<Drag>().enabled=false;
			}

	}

	void OnTriggerEnter2D(Collider2D other) {

		//Only if it encounters an empty orbit there is a collision
			if(other.gameObject.tag=="orbit"){
				if(!other.gameObject.GetComponent<Ring>().HasPlanet())
					lastCollision = other;
				else
					lastCollision = null;

			}

  }

}
