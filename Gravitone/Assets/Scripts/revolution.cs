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
		float lastPlayAngle = 0;
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

			float angleDelay = checkPosition(currentAngle);
			/*
			Il kick deve suonare solo se non sta gi� suonando,
			oppure se sta suonando ma l'angolo � maggiore del margine d'errore
			(Questo per evitare colpi missati su velocit� alte, in cui bisogna tagliare la coda)
			*/
      if (angleDelay != -1 &&
      	 (!(Mathf.Abs(currentAngle - lastPlayAngle) <= error) || !kick.isPlaying)) {
				    lastPlayAngle = currentAngle;
				    kick.PlayDelayed(angleDelay / angularSpeed);
				}
			}

		// Obtains the planet position from the planet's angle
		Vector3 getPosition (float angle) {
			return new Vector3(radius*Mathf.Sin(angle), radius*Mathf.Cos(angle),0);
		}

    /*
    Fa si che il kick suona ogni quarto
    ritorna l'offset tra l'angolo giusto e quello rilevato
    o -1 se non deve suonare
    */
    float checkPosition (float angleC)
    {
      float angle = angleC % TWO_PI;

      if (angle > TWO_PI - error && angle <= TWO_PI)
        return TWO_PI - angle;

      else if (angle > (Mathf.PI - error) && angle <= Mathf.PI)
        return Mathf.PI - angle;

      else if (angle > (Mathf.PI / 2 - error) && angle <= (Mathf.PI / 2))
        return (Mathf.PI / 2) - angle;

      else if (angle > (Mathf.PI * 3 / 2 - error) && angle <= (Mathf.PI * 3 / 2))
        return (Mathf.PI * 3 / 2) - angle;

      else
        return -1f;

    }

}
