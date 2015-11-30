using UnityEngine;
using System.Collections;

public class HarmonyPreview : MonoBehaviour {

	public GameObject previewPlanetPrefab;
	public GameObject audioManager;
	public float[] orbitsRadius;
	private int totalNotes = 4;
	private int[] notes = new int[4] {50,50,50,55};
	private string[] types = new string[4] {"M", "m", "M7", "M7"};
	private GameObject[] Planets = new GameObject[4];

	public void StartPreview() {
		for (int i = 0; i < totalNotes; i++) {
			Planets[i] = Instantiate(previewPlanetPrefab, new Vector3(orbitsRadius[i], 0, 0), Quaternion.identity) as GameObject;
			Planets[i].GetComponent<ChordPlanet>().audioManager = audioManager;
			Planets[i].GetComponent<ChordPlanet>().chordName = types[i];
			Planets[i].GetComponent<ChordPlanet>().baseNote = notes[i];
			Planets[i].GetComponent<ChordPlanet>().active = true;
		}
	}

	public void StopPreview() {
		foreach (GameObject planet in Planets) {
			Destroy(planet);
		}
	}

}
