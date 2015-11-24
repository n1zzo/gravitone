using UnityEngine;
using System.Collections;

public class Ring : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void SetSize(Vector3 size){
		transform.localScale=size;
	}
}
