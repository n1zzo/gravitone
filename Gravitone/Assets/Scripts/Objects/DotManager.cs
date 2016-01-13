using UnityEngine;
using System.Collections;

public class DotManager : MonoBehaviour {

	private float alphaTreshold = 0.2f;
	private float fadeSpeed = 0.1f;
	private float transparency = 0f;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		// Decrease alpha channel until treshold is reached
		if(transparency > alphaTreshold) {
			float decrement = fadeSpeed*Time.deltaTime;
			transparency -= decrement;
			this.GetComponent<AlphaColor>().SetTransparency(transparency);
		}
		else {
			transparency = alphaTreshold;
		}
	}

	public void Fill() {
		this.GetComponent<AlphaColor>().SetTransparency(1f);
		transparency = 1f;
	}
}
