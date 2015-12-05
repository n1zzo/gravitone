using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public GameObject audioManager;

	int level=1;

	// Use this for initialization
	void Start () {
			Screen.orientation = ScreenOrientation.LandscapeLeft ;

			// Testing only: skip to level 2 or 3
			GetComponent<Level1>().Autocomplete();
			//GetComponent<Level2>().NextLevel();
	}

	// Update is called once per frame
	void Update () {

	}

	public void goToNextLevel(){

		level++;
		if(level==2){
			GetComponent<Level1>().enabled=false;
			GetComponent<Level2>().enabled=true;
			GetComponent<Level3>().enabled=false;
		}
		else if(level==3){
			GetComponent<Level1>().enabled=false;
			GetComponent<Level2>().enabled=false;
			GetComponent<Level3>().enabled=true;
		}

	}

}
