using UnityEngine;
using System.Collections;

public class FreeDrag : MonoBehaviour {

	private Vector3 screenPoint;
	private Vector3 offset;
	float afterColliderRadius;
	public float[] radiusOrbits;
	public int orbitNumber=-1;
	GameObject levelManager;

	void Start(){

		GetComponent<Buttonize>().action = handleMouseDown;

		levelManager=GameObject.FindWithTag("LevelManager");

	}

	public void handleMouseDown () {

			if(orbitNumber!=-1){
				
				GetComponent<Rotate>().enabled=false;

				levelManager.GetComponent<FreeLevel2>().RemovePlaced();

			}

			screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

			offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

			GetComponent<ChordPlanet>().Play();

			GetComponent<SelfRotate>().enabled=true;

	}

	void OnMouseDrag () {
		if(GetComponent<FreeDrag>().enabled){
			Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
			Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
			transform.position = cursorPosition;
		}
	}

	void OnMouseUp () {
		if(GetComponent<FreeDrag>().enabled){

					orbitNumber=-1;

					GetComponent<SelfRotate>().enabled=false;

					// Assign the planet to the nearest orbit.
					// Get the radius from the orbit and give it to Rotate.cs
					// Deactivate the FreeDrag functionality
					int count=0;
					foreach (float orbit in radiusOrbits){
						float x=transform.position.x;
						float y=transform.position.y;
						if(Mathf.Abs(orbit-Mathf.Sqrt(x*x + y*y))<1f){

									AssignToOrbit(orbit, count);
									levelManager.GetComponent<FreeLevel2>().increasePlaced();
									break;

						}
						count++;
					}
		}
	}

	public void AssignToOrbit(float orbit, int count) {

		orbitNumber=count;

		// Enable the rotation with the right radius
		GetComponent<Rotate>().SetRadius(orbit);
		// Force offset angle refresh
		GetComponent<Rotate>().SetDirtyOffset();
		// Enable the planet rotation
		GetComponent<Rotate>().enabled=true;
		// Enable the planet revolution
		GetComponent<SelfRotate>().enabled=true;

	}

}
