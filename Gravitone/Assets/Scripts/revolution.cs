using UnityEngine;
using System.Collections;

public class revolution : MonoBehaviour {

		public int starX = 0;
		public int starY = 0;
		public int bpm = 120;
		public int beatsPerBar = 4;
		public float radius = 1f;
		const float TWO_PI = 2*Mathf.PI;
		float currentAngle = 0;
		float angularSpeed = 0;
		float error = 0.1f;
		AudioSource kick;

    // Use this for initialization
		void Start () {
			kick = GetComponent<AudioSource>();
			transform.position = new Vector3(0, radius, 0);
			// Angular speed is derived from BPMs
			angularSpeed = bpm * TWO_PI	/ (60 * beatsPerBar);

			// The first beat is always missing
			// We trigger it manually at initialization
			kick.Play();
		}

    // Update is called once per frame
    void Update () {
			// At every frame the planet's position is updated
			currentAngle += angularSpeed * Time.deltaTime;
			transform.position = getPosition(currentAngle);

			// Plays the kick only if it's not yet playing
			if (!kick.isPlaying && checkBeat(currentAngle)) {
				kick.Play();
			}
		}

		// Obtains the planet position from the planet's angle
		Vector3 getPosition (float angle) {
			return new Vector3(radius*Mathf.Sin(angle), radius*Mathf.Cos(angle),0);
		}

		// Checks if the current angle corresponds to a beat within a given error range
		bool checkBeat (float angle) {
			return (angle % (TWO_PI / beatsPerBar) < error);
		}

}
