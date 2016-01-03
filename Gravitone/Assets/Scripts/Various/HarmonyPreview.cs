using UnityEngine;
using System.Collections;

public class HarmonyPreview : MonoBehaviour {

	public GameObject previewPlanetPrefab;
	private int[] notes;
	private string[] types;
	private int i = 0;
	GameObject[] previews = new GameObject[4];

	public void PlayPlanet(float radius) {

		switch(i){
			case 0:previews[i] = Instantiate(previewPlanetPrefab, new Vector3(0, -radius, 0), Quaternion.identity) as GameObject; break;
			case 1:previews[i]= Instantiate(previewPlanetPrefab, new Vector3(-radius,0, 0), Quaternion.identity) as GameObject; break;
			case 2:previews[i] = Instantiate(previewPlanetPrefab, new Vector3(0, radius, 0), Quaternion.identity) as GameObject; break;
			case 3:previews[i] = Instantiate(previewPlanetPrefab, new Vector3(radius, 0, 0), Quaternion.identity) as GameObject; break;
		}

		previews[i].GetComponent<PrevPlanet>().baseNote=notes[i];
		previews[i].GetComponent<PrevPlanet>().chordName=types[i];
		previews[i].GetComponent<PrevPlanet>().position=i;
		previews[i].GetComponent<PrevPlanet>().Play();

		i++;
	}

	public void StopPreview() {
		i=0;
	}

	public void setPreview(int[] note, string[] typs){
		notes=note;
		types=typs;
	}

	public GameObject[] GetPreviews(){
		return previews;
	}
}
