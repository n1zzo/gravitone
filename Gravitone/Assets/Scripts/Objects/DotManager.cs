using UnityEngine;
using System.Collections;

public class DotManager : MonoBehaviour {

	public float fadeSpeed = 2f;
	public float alphaTreshold = 0.2f;
	private float transparency = 0f;
	private bool isPink = false;

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

	public void MakePink() {
		if(!isPink) {
			this.GetComponent<AlphaColor>().SetColor(255f, 163f, 219f);
			transform.localScale += new Vector3(0.8f, 0.8f, 0);
			alphaTreshold = 0.4f;
			isPink = true;
		}
	}
}
