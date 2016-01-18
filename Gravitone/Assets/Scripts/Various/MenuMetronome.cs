using UnityEngine;
using LibPDBinding;
using System.Collections;

public class MenuMetronome : Subscriber {

	// Use this for initialization
	void Start () {
		GetComponent<BeatGen>().Subscribe(this);
	}

	// Update is called once per frame
	void Update () {

	}

	public override void Beat(int cs) {
		if(cs==0)
			LibPD.SendBang("highBeat");
	}

}
