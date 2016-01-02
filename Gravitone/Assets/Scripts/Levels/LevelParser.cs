using UnityEngine;
using System.Collections;
using SimpleJSON;

public class LevelParser : MonoBehaviour {

	public int number = 1;
	public GameObject planetKick;
	public GameObject planetSnare;
	public GameObject planetHat;
	private string toParse;

	// Use this for initialization
	void Start () {
		 LoadLevel();
	}

	void LoadLevel() {
		TextAsset currentLevel = Resources.Load("Levels/Level"+number) as TextAsset;
		toParse = currentLevel.text;
		var N = JSON.Parse(toParse);
		for (int i = 0; i < 64; i++) {
			Debug.Log(N["data"]["lol"]);
			if (N["data"]["kickArray"][i].AsInt == 0)
				planetKick.GetComponent<Drum>().targetDrumArray[i] = false;
			else
				planetKick.GetComponent<Drum>().targetDrumArray[i] = true;
		}
		for (int i = 0; i < 64; i++) {
			if (N["data"]["snareArray"][i].AsInt == 0)
				planetSnare.GetComponent<Drum>().targetDrumArray[i] = false;
			else
				planetSnare.GetComponent<Drum>().targetDrumArray[i] = true;
		}
		for (int i = 0; i < 64; i++) {
			if (N["data"]["hatArray"][i].AsInt == 0)
				planetHat.GetComponent<Drum>().targetDrumArray[i] = false;
			else
				planetHat.GetComponent<Drum>().targetDrumArray[i] = true;
		}
	}

}
