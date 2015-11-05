using UnityEngine;
using System.Collections;

public class revolution : MonoBehaviour {

	public int starX = 0;
	public int starY = 0;
	public int bpm = 60;
	public int beatsPerBar = 4;
	public float radius = 0.4f;
	float twoPI = 2*Mathf.PI;
	float currentAngle = 0;
	float angularSpeed = 0;

	// Use this for initialization
	void Start () {
		transform.position = new Vector3(0, radius, 0);
		angularSpeed = bpm * twoPI	/ (60 * beatsPerBar);
	}

	// Update is called once per frame
	void Update () {
		currentAngle += angularSpeed * Time.deltaTime;
		transform.position = getPosition(currentAngle);
	}

	Vector3 getPosition (float angle) {
		return new Vector3(radius*Mathf.Sin(angle), radius*Mathf.Cos(angle),0);
	}

}
