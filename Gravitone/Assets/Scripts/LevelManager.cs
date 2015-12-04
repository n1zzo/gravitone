using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public GameObject audioManager;

	int level=1;

	// Use this for initialization
	void Start () {
			Screen.orientation = ScreenOrientation.LandscapeLeft ;

			GetComponent<Level1>().Autocomplete();
	}

	// Update is called once per frame
	void Update () {

	}

	public void goToNextLevel(){

		level++;
		if(level==2){
			GetComponent<Level1>().enabled=false;
			GetComponent<Level2>().enabled=true;
		}

	}

}
