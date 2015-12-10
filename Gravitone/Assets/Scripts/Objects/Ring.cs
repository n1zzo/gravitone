using UnityEngine;
using System.Collections;

public class Ring : MonoBehaviour {

	private Vector3 size;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void SetSize(Vector3 size){
		this.size = size;
		transform.localScale=size;
	}

	public float GetRadius() {
		return GetComponent<SpriteRenderer>().bounds.extents.x;
	}

}
