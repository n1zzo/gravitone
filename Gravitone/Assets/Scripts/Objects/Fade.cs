using UnityEngine;
using System.Collections;

public class Fade : MonoBehaviour {

	float speed=0.03f;
	public float final=0f;

	// Use this for initialization
	void Start () {
		Color newColor = GetComponent<Renderer>().material.color;
		newColor.a=0;
		GetComponent<Renderer>().material.SetColor("_Color", newColor);
	}

	// Update is called once per frame
	void Update () {

		Color newColor = GetComponent<Renderer>().material.color;

		newColor.a+=speed;

		if(newColor.a<final)
			GetComponent<Renderer>().material.SetColor("_Color", newColor);
		else
			GetComponent<Fade>().enabled=false;
			
	}
}
