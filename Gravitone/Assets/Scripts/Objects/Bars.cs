using UnityEngine;
using System.Collections;

public class Bars : MonoBehaviour {

	public GameObject planet;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		transform.position=planet.transform.position + new Vector3(-2, 0, 0);
	}
}
