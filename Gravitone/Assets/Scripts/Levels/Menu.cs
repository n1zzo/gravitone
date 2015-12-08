using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class Menu : MonoBehaviour {

	public GameObject cam;
	public GameObject menucam;
	public GameObject menutree;
	public GameObject onlinelight;
	public GameObject soundslight;

	public void Enable() {
		// Turn on the gaussian blur effect
		cam.GetComponent<BlurOptimized>().enabled = true;
		// Enable the menu camera
		menucam.SetActive(true);
		// Enable the target objects
		menutree.SetActiveRecursively(true);
		// Disable the active version of the buttons
		onlinelight.SetActive(false);
		soundslight.SetActive(false);
	}

	public void Disable() {
		// Turn on the gaussian blur effect
		cam.GetComponent<BlurOptimized>().enabled = false;
		// Enable the menu camera
		menucam.SetActive(false);
		// Enable the target objects
		menutree.SetActiveRecursively(false);
	}

}
