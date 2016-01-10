using UnityEngine;
using System.Collections;

public class Level3 : MonoBehaviour {

	public GameObject cam;
	public GameObject[] planets;
	public GameObject background;
	public GameObject melody;
	public GameObject canvas;
	public GameObject bass;
	private GameObject audioManager;


	// Use this for initialization
	void Start () {

		audioManager = GetComponent<LevelManager>().audioManager;

		// Set intruments gain levels
		audioManager.GetComponent<AudioManager>().SetDrumVolume(0.4f);
		audioManager.GetComponent<AudioManager>().SetChordsVolume(0.6f);
		audioManager.GetComponent<AudioManager>().SetStringsVolume(0.8f);
		audioManager.GetComponent<AudioManager>().SetBassVolume(0.6f);

		melody.SetActive(true);
		canvas.SetActive(true);
		bass.SetActive(true);

		background.GetComponent<Fade>().enabled=true;
		background.GetComponent<Fade>().final=0.3f;
		background.GetComponent<SpriteRenderer>().enabled=true;

		GameObject[] planetCopy=planets;

		foreach(GameObject planet in planets){
			planet.GetComponent<SpriteRenderer>().enabled=false;
			planet.GetComponent<Rotate>().enabled=false;
			planet.GetComponent<Drag>().enabled=false;
			planetCopy[planet.GetComponent<ChordPlanet>().order]=planet;
		}

		planets=planetCopy;

		// Set the camera and menu to follow the first planet.
		cam.GetComponent<SmoothFollow2D>().target = planets[0].transform;
		cam.GetComponent<SmoothFollow2D>().enabled = true;
		cam.GetComponent<SmoothCamera>().setArrival(6f);
		cam.GetComponent<SmoothCamera>().enabled=true;

		planets[0].GetComponent<SpriteRenderer>().enabled=true;

		planets[0].SetActiveRecursively(true);

		melody.GetComponent<Melodies>().planets=planets;

		melody.GetComponent<Melodies>().TogglePreview();

	}

	// Update is called once per frame
	void Update () {

	}

	public void Restart(){
		melody.GetComponent<Melodies>().calculateTotalNotes(true);
		melody.GetComponent<Melodies>().TogglePreview();
	}

	public void ChangeCamera(int number){
		planets[number-1].GetComponent<SpriteRenderer>().enabled=false;
		planets[number].GetComponent<SpriteRenderer>().enabled=true;
		cam.GetComponent<SmoothFollow2D>().target = planets[number].transform;
	}

	private void EliminateBars() {
		foreach (GameObject bar in GameObject.FindGameObjectsWithTag("Bar")) {
			bar.GetComponent<SpriteRenderer>().enabled=false;
		}
	}

	public void NextLevel(){
		EliminateBars();

		foreach(GameObject planet in planets){
			planet.GetComponent<SpriteRenderer>().enabled=true;
			planet.GetComponent<Rotate>().enabled=true;
			planet.GetComponent<Drag>().enabled=false;
		}
		// Set the camera and menu to follow the first planet.
		cam.GetComponent<SmoothFollow2D>().enabled = false;
		cam.transform.position=new Vector3(0,0,-10);
		cam.GetComponent<SmoothCamera>().setArrival(10.5f);
		cam.GetComponent<SmoothCamera>().enabled=true;

		canvas.SetActive(false);

		bass.GetComponent<BassPlayer>().ResetCurrentBar();

		GetComponent<LevelManager>().goToNextLevel();

		background.GetComponent<SpriteRenderer>().enabled=false;

	}
}
