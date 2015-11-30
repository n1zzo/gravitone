using UnityEngine;
using System.Collections;

public class Drag : MonoBehaviour {

	private Vector3 screenPoint;
	private Vector3 offset;
	float afterColliderRadius;
	public float[] radiusOrbits;
	public int orbitNumber=0;

	void Start(){}

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
			int count=0;
			foreach (float orbit in radiusOrbits){
				float x=transform.position.x;
				float y=transform.position.y;
				if(Mathf.Abs(orbit-Mathf.Sqrt(x*x + y*y))<1f){

					Debug.Log(orbit);

					orbitNumber=count;

					GetComponent<Rotate>().enabled=true;

					GetComponent<Rotate>().setRadius(orbit);

					// We can adjust this to avoid the CHORD DELAY !!!!
					afterColliderRadius=1f;

					GetComponent<CircleCollider2D>().radius=afterColliderRadius;

					GetComponent<ChordPlanet>().active=true;

					if(GetComponent<ChordPlanet>().active)
						GetComponent<Drag>().enabled=false;

					break;

				}
				count++;
			}


	}

}
