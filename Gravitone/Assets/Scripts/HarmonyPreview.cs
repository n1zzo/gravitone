using UnityEngine;
using System.Collections;

public class HarmonyPreview : MonoBehaviour {

	public GameObject previewPlanetPrefab;
	public GameObject audioManager;
	private int[] notes = new int[4] {50,50,50,55};
	private string[] types = new string[4] {"M", "m", "M7", "M7"};
	private GameObject[] Planets = new GameObject[4];
	private int i = 0;

	public void AddPlanet(float radius) {
		Planets[i] = Instantiate(previewPlanetPrefab, new Vector3(radius, 0, 0), Quaternion.identity) as GameObject;
		Planets[i].GetComponent<ChordPlanet>().audioManager = audioManager;
		Planets[i].GetComponent<ChordPlanet>().chordName = types[i];
		Planets[i].GetComponent<ChordPlanet>().baseNote = notes[i];
		Planets[i].GetComponent<ChordPlanet>().active = true;
		// Let the planet play as soon as it is created
		Planets[i].GetComponent<ChordPlanet>().Play();
		// Increment the planets counter
		i++;
	}

	public void StopPreview() {
		foreach (GameObject planet in Planets) {
			Destroy(planet);
		}
	}

}
