using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	int level=1;

	// Use this for initialization
	void Start () {
			Screen.orientation = ScreenOrientation.LandscapeLeft ;
			goToNextLevel();

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
