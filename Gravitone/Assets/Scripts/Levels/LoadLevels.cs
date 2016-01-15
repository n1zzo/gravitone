using UnityEngine;
using System.Collections;


public class LoadLevels : MonoBehaviour {

    public GameObject globals;

	public void NextLevelButton(int level) {
         globals.GetComponent<Globals>().SetLevelNumber(level);
         Application.LoadLevel("LevelTemplate");
    }

}
