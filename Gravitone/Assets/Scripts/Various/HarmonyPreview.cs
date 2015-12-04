using UnityEngine;
using System.Collections;

public class HarmonyPreview : MonoBehaviour {

	public GameObject previewPlanetPrefab;
	public GameObject audioManager;
	private int[] notes = new int[4] {50,50,50,55};
	private string[] types = new string[4] {"M", "m", "M7", "M7"};
	private int i = 0;

	public void PlayPlanet(float radius) {
		switch(i){
			case 0:Instantiate(previewPlanetPrefab, new Vector3(0, -radius, 0), Quaternion.identity); break;
			case 1:Instantiate(previewPlanetPrefab, new Vector3(-radius,0, 0), Quaternion.identity); break;
			case 2:Instantiate(previewPlanetPrefab, new Vector3(0, radius, 0), Quaternion.identity); break;
			case 3:Instantiate(previewPlanetPrefab, new Vector3(radius, 0, 0), Quaternion.identity); break;
		}

		audioManager.GetComponent<AudioManager>().PlayChord(notes[i], types[i]);
		i++;
	}

	public void StopPreview() {
		i=0;
	}

	public void setPreview(int[] note, string[] typs){
		notes=note;
		types=typs;
	}

}
