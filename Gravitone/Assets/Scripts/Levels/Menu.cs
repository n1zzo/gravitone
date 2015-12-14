using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class Menu : MonoBehaviour {

	public GameObject cam;
	public GameObject menu;
	public GameObject menutree;
	public GameObject text;

	public void Enable() {

		// Turn on the gaussian blur epublic GameObject audioManager;
		if (Application.platform == RuntimePlatform.Android)
			cam.GetComponent<Blur>().enabled = true;
		else
			cam.GetComponent<BlurOptimized>().enabled = true;
		// Enable the menu camera
		menu.SetActive(true);
		// Enable the target objects
		menutree.SetActive(true);

		//Turn off the text on the first phase
		text.SetActive(false);

		Time.timeScale=0f;
	}

	public void Disable() {

		// Turn on the gaussian blur
		if (Application.platform == RuntimePlatform.Android)
			cam.GetComponent<Blur>().enabled = false;
		else
			cam.GetComponent<BlurOptimized>().enabled = false;

		// Disable the menu camera
		menu.SetActive(false);

		// Disable the target objects
		menutree.SetActive(false);

		//Turn on the text on the first phase
		text.SetActive(true);


		Time.timeScale=1f;
	}

}
