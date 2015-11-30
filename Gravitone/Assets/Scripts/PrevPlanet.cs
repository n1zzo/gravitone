using UnityEngine;
using System.Collections;

public class PrevPlanet : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		Color newColor = GetComponent<Renderer>().material.color;
		newColor.a-=0.01f;
		if(newColor.a>0)
			GetComponent<Renderer>().material.SetColor("_Color", newColor);
		else
			Destroy(this.gameObject);

	}
}
