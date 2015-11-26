using UnityEngine;
using System.Collections;

public class Ring : MonoBehaviour {

	private GameObject planet;
	private Vector3 size;
	private bool hasPlanet=false;

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

	public void SetItHas(){
		hasPlanet=true;
	}

	public bool HasPlanet(){
		return hasPlanet;
	}


}
