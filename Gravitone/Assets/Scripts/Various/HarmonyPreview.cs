using UnityEngine;
using System.Collections;

public class HarmonyPreview : MonoBehaviour {

	public GameObject previewPlanetPrefab;
	private int[] notes;
	private string[] types;
	private int i = 0;

	public void PlayPlanet(float radius) {

		GameObject preview;

		switch(i){
			case 0:preview = Instantiate(previewPlanetPrefab, new Vector3(0, -radius, 0), Quaternion.identity) as GameObject; break;
			case 1:preview = Instantiate(previewPlanetPrefab, new Vector3(-radius,0, 0), Quaternion.identity) as GameObject; break;
			case 2:preview = Instantiate(previewPlanetPrefab, new Vector3(0, radius, 0), Quaternion.identity) as GameObject; break;
			case 3:preview = Instantiate(previewPlanetPrefab, new Vector3(radius, 0, 0), Quaternion.identity) as GameObject; break;
			default: preview= Instantiate(previewPlanetPrefab, new Vector3(radius, 0, 0), Quaternion.identity) as GameObject; break;
		}

		preview.GetComponent<PrevPlanet>().baseNote=notes[i];
		preview.GetComponent<PrevPlanet>().chordName=types[i];
		preview.GetComponent<PrevPlanet>().position=i;
		preview.GetComponent<PrevPlanet>().Play();

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
