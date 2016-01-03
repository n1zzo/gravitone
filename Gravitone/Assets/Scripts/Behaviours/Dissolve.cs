using UnityEngine;
using System.Collections;

public class Dissolve : MonoBehaviour {

	public float speed = 0.01f;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

			Color newColor = GetComponent<Renderer>().material.color;
			newColor.a-=speed;
			if(newColor.a>0)
				GetComponent<Renderer>().material.SetColor("_Color", newColor);
			else
				Destroy(this.gameObject);

	}
}
