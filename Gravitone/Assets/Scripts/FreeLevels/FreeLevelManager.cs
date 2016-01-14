using UnityEngine;
using System.Collections;

public class FreeLevelManager : MonoBehaviour {

	public GameObject audioManager;
	public GameObject background;

	int level=1;

	// Use this for initialization
	void Start () {
			Screen.orientation = ScreenOrientation.LandscapeLeft ;
	}

	// Update is called once per frame
	void Update () {

	}

	public void goToNextLevel(){

		level++;
		if(level==2){
			GetComponent<FreeLevel1>().enabled=false;
			GetComponent<FreeLevel2>().enabled=true;
			GetComponent<Level3>().enabled=false;
		}
		else if(level==3){
			GetComponent<FreeLevel1>().enabled=false;
			GetComponent<FreeLevel2>().enabled=false;
			GetComponent<Level3>().enabled=true;
		} else if (level==4) {
			GetComponent<FreeLevel1>().enabled=false;
			GetComponent<FreeLevel2>().enabled=false;
			GetComponent<Level3>().enabled=false;
		}
	}

	public void Restart(){
		switch(level){
			case 1: GetComponent<FreeLevel1>().Restart(); break;
			case 2: GetComponent<FreeLevel2>().Restart(); break;
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

	public int GetLevel(){
		return level;
	}

}
