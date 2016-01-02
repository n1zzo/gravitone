using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class BloomContol : MonoBehaviour {

	private GameObject mainCamera;
	private float MAX_INTENSITY = 2.5f;
	private float intensity;
	private float step;

	// Use this for initialization
	void Start () {
		mainCamera = GameObject.FindWithTag("MainCamera");
		Debug.Log(mainCamera.GetComponent<BloomOptimized>().intensity);
	}

	// Update is called once per frame
	void Update () {
		if(mainCamera.GetComponent<BloomOptimized>().intensity <= 0f) {
			mainCamera.GetComponent<BloomOptimized>().intensity = 0f;
		} else {
			mainCamera.GetComponent<BloomOptimized>().intensity -= step;
		}
	}

	public void BloomPulse() {
		mainCamera.GetComponent<BloomOptimized>().intensity = MAX_INTENSITY;
	}

}
