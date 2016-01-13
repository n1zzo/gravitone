using UnityEngine;
using System.Collections;

public class MetroDot : MonoBehaviour {

	public Object pointPrefab;
	private GameObject[] points = new GameObject[64];
	private float radius;
	private float dotOffset = 0.1f;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	// Instantiate the dots into the scene at the correct position
	public void PlaceDots(int beatsPerBar, int subBeatsPerBeat, float radius) {
		float angleQuantum = (Mathf.PI * 2) / beatsPerBar;
		float offsetQuantum = Mathf.PI / (2 * subBeatsPerBeat);
		float currentAngle = 0;
		float currentOffset = 0;
		for (int i = 0; i < beatsPerBar; i++) {
			currentOffset = - ((offsetQuantum * subBeatsPerBeat) / 2);
				for (int j = 0; j < subBeatsPerBeat; j++) {
					float x = radius * Mathf.Sin(currentAngle + currentOffset);
					float y = radius * Mathf.Cos(currentAngle + currentOffset);
					points[i+j] = (GameObject)Instantiate(pointPrefab, new Vector3(x, y, 0), Quaternion.identity);
					currentOffset += offsetQuantum;
				}
				currentAngle += angleQuantum;
		}
	}

	public void FillDot(int number) {
		GameObject fullDot = points[number].transform.GetChild(0).gameObject;
		GameObject emptyDot = points[number].transform.GetChild(1).gameObject;
		fullDot.GetComponent<DotManager>().Fill();
	}
}
