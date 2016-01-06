using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public GameObject audioManager;
	public GameObject background;

	int level=1;

	// Use this for initialization
	void Start () {
			Screen.orientation = ScreenOrientation.LandscapeLeft ;

			// Testing only: skip to level 2 or 3

			//GetComponent<Level1>().Autocomplete();
		  //GetComponent<Level2>().autocomplete=true;
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
		} else if (level==4) {
			GetComponent<Level1>().enabled=false;
			GetComponent<Level2>().enabled=false;
			GetComponent<Level3>().enabled=false;
		}
	}

	public void Restart(){
		switch(level){
			case 1: GetComponent<Level1>().Restart(); break;
			case 2: GetComponent<Level2>().Restart(); break;
			case 3: GetComponent<Level3>().Restart(); break;
			default: break;
		}
	}

	public void SetGreenBackground(){
		background.GetComponent<SpriteRenderer>().color=Color.green;
	}

	public void SetRedBackground(){
		background.GetComponent<SpriteRenderer>().color=Color.red;
	}

	public void SetGreyBackground(){
		background.GetComponent<SpriteRenderer>().color=Color.grey;
	}

	public void ResetBackground(){
		background.GetComponent<SpriteRenderer>().color=Color.white;
	}

	public void Autocomplete(){
		if(level==1)
			GetComponent<Level1>().Autocomplete();
		else if (level==2)
			GetComponent<Level2>().autocomplete=true;
	}

	public int GetLevel(){
		return level;
	}

}
