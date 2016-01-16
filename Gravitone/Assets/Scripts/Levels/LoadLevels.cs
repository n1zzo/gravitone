using UnityEngine;
using System.Collections;


public class LoadLevels : MonoBehaviour {

    public GameObject title;
    public GameObject playButton;
    public GameObject[] levelButtons;
    public GameObject globals;
    public GameObject back;

    public void ShowLevels() {
        title.SetActive(false);
        playButton.SetActive(false);
        foreach(GameObject button in levelButtons) {
                button.SetActive(true);
        }
        back.SetActive(true);
    }

    public void Back() {
        back.SetActive(false);
        foreach(GameObject button in levelButtons) {
                button.SetActive(false);
        }
        title.SetActive(true);
        playButton.SetActive(true);
    }

    public void StartFreeMode() {
        Application.LoadLevel("FreePlay");
    }

	public void StartLevel(int level) {
         globals.GetComponent<Globals>().SetLevelNumber(level);
         Application.LoadLevel("LevelTemplate");
    }

}
