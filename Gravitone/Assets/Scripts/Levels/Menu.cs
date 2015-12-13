using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class Menu : MonoBehaviour {

	public GameObject cam;
	public GameObject menu;
	public GameObject menutree;
	public GameObject text;
	public GameObject background;

	public void Enable() {
		// Turn on the gaussian blur effect
		cam.GetComponent<BlurOptimized>().enabled = true;
		// Enable the menu camera
		menu.SetActiveRecursively(true);
		// Enable the target objects
		menutree.SetActive(true);
		background.SetActive(true);

		//Turn off the text on the first phase
		text.SetActive(false);

		Time.timeScale=0f;
	}

	public void Disable() {
		background.SetActive(false);

		// Turn off the gaussian blur effect
		cam.GetComponent<BlurOptimized>().enabled = false;

		// Disable the menu camera
		menu.SetActiveRecursively(false);

		// Disable the target objects
		menutree.SetActive(false);

		//Turn on the text on the first phase
		text.SetActive(true);


		Time.timeScale=1f;
	}

}
