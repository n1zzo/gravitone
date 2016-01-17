using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class Menu : MonoBehaviour {

	public GameObject cam;
	public GameObject menu;
	public GameObject menutree;
	public GameObject text;
	public GameObject melodyCanvas;
	private bool isCanvas;
	private GameObject audioManager;

	void Start(){
		audioManager=GameObject.FindWithTag("AudioManager");
	}

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

		if(melodyCanvas.activeSelf){
			isCanvas=true;
			melodyCanvas.SetActive(false);
		} else
			isCanvas=false;

		//Turn off the text on the first phase
		text.SetActive(false);

		audioManager.GetComponent<AudioManager>().StopBass();

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

		if(isCanvas){
			melodyCanvas.SetActive(true);
			isCanvas=false;
		}

		Time.timeScale=1f;
	}

	public void ReturnToMainMenu() {
			Destroy(GameObject.FindWithTag("Globals"));
			Time.timeScale=1f;
			Application.LoadLevel("menu");
	}

}
