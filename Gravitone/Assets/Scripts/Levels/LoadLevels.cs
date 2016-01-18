using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class LoadLevels : MonoBehaviour {

    public GameObject title;
    public GameObject playButton;
    public GameObject[] levelButtons;
    public GameObject globals;
    public GameObject back;
    public GameObject bpmValue;
    public List<GameObject> BPM;
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

    public void ShowBPMSelect() {
        foreach(GameObject button in levelButtons) {
                button.SetActive(false);
        }
        back.SetActive(false);
        foreach(GameObject element in BPM) {
            element.SetActive(true);
        }
    }

    public void BackToLevels() {
        foreach(GameObject element in BPM) {
            element.SetActive(false);
        }
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

    public void IncrementBPM() {
        int newValue = globals.GetComponent<Globals>().bpm + 1;
        globals.GetComponent<Globals>().bpm = newValue;
        bpmValue.GetComponent<Text>().text = newValue.ToString();
    }

    public void DecrementBPM() {
        int newValue = globals.GetComponent<Globals>().bpm - 1;
        globals.GetComponent<Globals>().bpm = newValue;
        bpmValue.GetComponent<Text>().text = newValue.ToString();
    }

}
