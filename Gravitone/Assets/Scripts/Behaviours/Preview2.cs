using UnityEngine;
using System.Collections;

public class Preview2 : MonoBehaviour {

	GameObject levelManager;

	// Use this for initialization
	void Start () {
		levelManager=GameObject.FindWithTag("LevelManager");
	}

	void OnMouseDown () {
		if(levelManager.GetComponent<LevelManager>().GetLevel()==2)
			levelManager.GetComponent<Level2>().PlayPreview();
	}
}
