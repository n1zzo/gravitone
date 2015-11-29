using UnityEngine;
using System.Collections;

public class HarmonyPreview : MonoBehaviour {

	public float[] orbitsRadius;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		foreach(float radius in orbitsRadius) {
			//Debug.Log(radius);
		}
	}
}
