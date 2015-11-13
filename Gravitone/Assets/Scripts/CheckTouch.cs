using UnityEngine;
using System.Collections;

public class CheckTouch : MonoBehaviour {

	public UnityEngine.UI.Text posText;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		for (int i = 0; i < Input.touchCount; ++i)
			if(Input.GetTouch(i).phase == TouchPhase.Began)
					posText.text= Input.GetTouch(i).position.x.ToString();
	}
}
