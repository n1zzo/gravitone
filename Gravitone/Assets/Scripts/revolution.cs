using UnityEngine;
using System.Collections;

public class revolution : MonoBehaviour {

	public int starX = 0;
	public int starY = 0;
	public int bpm = 120;
	public int beatsPerBar = 4;
	public float radius = 1f;
    AudioSource kick;
	float twoPI = 2*Mathf.PI;
	float currentAngle = 0;
	float angularSpeed = 0;
    float error = 0.1f;
    float lastPlayAngle = 0;

    // Use this for initialization
    void Start () {
        kick = GetComponent<AudioSource>();
		transform.position = new Vector3(0, radius, 0);
		angularSpeed = bpm * twoPI	/ (60 * beatsPerBar);

        // La prima volta missa il colpo
        // a noi servirà il primo play quando verrà selezionato
        // dopodichè verrà quantizzato
        kick.Play();
	}


        // Update is called once per frame
    void Update () {
		currentAngle += angularSpeed * Time.deltaTime;
        transform.position = getPosition(currentAngle);
        float angleDelay = checkPosition(currentAngle);
        
        /*
        Il kick deve suonare solo se non sta già suonando, 
        oppure se sta suonando ma l'angolo è maggiore del margine d'errore 
        (Questo per evitare colpi missati su velocità alte, in cui bisogna tagliare la coda)
        */
        if (angleDelay != -1 && 
                (!(Mathf.Abs(currentAngle - lastPlayAngle) <= error) || !kick.isPlaying))
            {
                lastPlayAngle = currentAngle;
                kick.PlayDelayed(angleDelay / angularSpeed);
            }
                
	}

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
        float angle = angleC % twoPI;

        if (angle > twoPI - error && angle <= twoPI)
            return twoPI - angle;

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
