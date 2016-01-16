using UnityEngine;
using System.Collections;


public class LoadLevels : MonoBehaviour {

    public GameObject title;
    public GameObject playButton;
    public GameObject[] levelButtons;
    public GameObject globals;

    public void ShowLevels() {
        title.SetActive(false);
        playButton.SetActive(false);
        Debug.Log("LOL");
        foreach(GameObject button in levelButtons) {
                button.SetActive(true);
        }
    }

	public void StartLevel(int level) {
         globals.GetComponent<Globals>().SetLevelNumber(level);
         Application.LoadLevel("LevelTemplate");
    }

}
