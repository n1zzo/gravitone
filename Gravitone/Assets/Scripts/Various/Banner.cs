using UnityEngine;
using System.Collections;

public class Banner : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine(HideBanner());
	}

	IEnumerator HideBanner() {
			yield return new WaitForSeconds(3.0f);
			Application.LoadLevel("menu");
	}
}
