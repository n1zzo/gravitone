using UnityEngine;
using System.Collections;

public class SmoothCamera : MonoBehaviour {

	private const float PER_FRAME=0.1f;
	float arrival=0;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		float size=GetComponent<Camera>().orthographicSize;
		if(size<arrival-PER_FRAME*2)
			GetComponent<Camera>().orthographicSize += PER_FRAME;
		else if(size>arrival+PER_FRAME*2)
			GetComponent<Camera>().orthographicSize -= PER_FRAME;
		else
			this.enabled = false;
	}

	public void setArrival (float arrive){
		arrival=arrive;
	}

}
