using UnityEngine;
using System.Collections;
using LibPDBinding;

public class LoadMetronome : MonoBehaviour {

	bool start=false;

	// Use this for initialization
	void Start () {

			LibPD.SendFloat("metroSound", 80f);
			LibPD.SendBang("metroControl");
			
	}

	// Update is called once per frame
	void Update () {

	}
}
