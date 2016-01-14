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
		int count = 0;
		float angleQuantum = (Mathf.PI * 2) / beatsPerBar;
		float offsetQuantum = Mathf.PI / (3 * subBeatsPerBeat);
		float currentAngle = - 0.5f;
		float currentOffset = 0;
		for (int i = 0; i < beatsPerBar; i++) {
			currentOffset = - ((offsetQuantum * subBeatsPerBeat) / 2);
				for (int j = 0; j < subBeatsPerBeat; j++) {
					float x = radius * Mathf.Sin(currentAngle + currentOffset);
					float y = radius * Mathf.Cos(currentAngle + currentOffset);
					points[count] = (GameObject)Instantiate(pointPrefab, new Vector3(x, y, 0), Quaternion.identity);
					count++;
					currentOffset += offsetQuantum;
				}
				currentAngle += angleQuantum;
		}
	}

	public void FillDot(int number) {
		GameObject fullDot = points[number].GetComponent<DotGroup>().fullDot;
		GameObject emptyDot = points[number].GetComponent<DotGroup>().emptyDot;
		fullDot.GetComponent<DotManager>().Fill();
	}
}
