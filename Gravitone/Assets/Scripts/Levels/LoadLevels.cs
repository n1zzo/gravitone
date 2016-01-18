using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LoadLevels : MonoBehaviour {

    public GameObject title;
    public GameObject playButton;
    public GameObject[] levelButtons;
    public GameObject globals;
    public GameObject back;
    private List<GameObject> titles = new List<GameObject>();
    private List<GameObject> toRemove = new List<GameObject>();

    public void Start() {
        InvokeRepeating("SpawnTitle", 1, 1);
    }

    public void Update() {
        // Fade out all the titles and enlarge them
        foreach(GameObject t in titles) {
            t.GetComponent<AlphaColor>().DecreaseTransparency(0.01f);
            t.transform.localScale += new Vector3(0.0015f,0.0015f,0);
            if(t.GetComponent<AlphaColor>().isInvisible())
                toRemove.Add(t);
        }
        foreach(GameObject t in toRemove) {
            titles.Remove(t);
            Destroy(t);
        }
        toRemove = new List<GameObject>();
    }

    private void SpawnTitle() {
        titles.Add(Instantiate(title, new Vector3(0, 2.5f, 0), Quaternion.identity) as GameObject);
    }

    public void ShowLevels() {
        title.SetActive(false);
        playButton.SetActive(false);
        foreach(GameObject button in levelButtons) {
                button.SetActive(true);
        }
        back.SetActive(true);
        foreach(GameObject t in titles) {
            Destroy(t);
        }
        titles = new List<GameObject>();
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
