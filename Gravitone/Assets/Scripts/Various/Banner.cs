using UnityEngine;
using System.Collections;

public class Banner : MonoBehaviour {

    public float time = 3.0f;

	// Use this for initialization
	void Start () {
		StartCoroutine(HideBanner());
        StartCoroutine(LoadMenu());
	}

	IEnumerator HideBanner() {
			yield return new WaitForSeconds(time);
            this.GetComponent<SpriteRenderer>().enabled = false;
	}

    IEnumerator LoadMenu() {
			yield return new WaitForSeconds(time*2);
			Application.LoadLevel("menu");
	}
}
