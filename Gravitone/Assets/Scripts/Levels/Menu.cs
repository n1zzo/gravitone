using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class Menu : MonoBehaviour {

	public GameObject cam;
	public GameObject structure;

	public void Enable() {
		// Turn on the gaussian blur effect
		cam.GetComponent<BlurOptimized>().enabled = true;
		structure.SetActive(true);
	}

	public void Disable() {

	}

}
