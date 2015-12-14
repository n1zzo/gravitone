using UnityEngine;
using System;
using System.Collections;

public class Buttonize : MonoBehaviour {

	public Action action;

	void OnMouseDown () {
		action();
	}

}
