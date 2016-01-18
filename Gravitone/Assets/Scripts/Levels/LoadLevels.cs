﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class LoadLevels : MonoBehaviour {

    public GameObject title;
    public GameObject playButton;
    public GameObject[] levelButtons;
    public GameObject globals;
    public GameObject back;
    public GameObject metronome;
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
        metronome.SetActive(true);
    }

    public void BackToLevels() {
        foreach(GameObject element in BPM) {
            element.SetActive(false);
        }
        foreach(GameObject button in levelButtons) {
                button.SetActive(true);
        }
        back.SetActive(true);
        metronome.SetActive(false);
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
      if(globals.GetComponent<Globals>().bpm<180){
        int newValue = globals.GetComponent<Globals>().bpm + 10;
        globals.GetComponent<Globals>().bpm = newValue;
        bpmValue.GetComponent<Text>().text = newValue.ToString();
        metronome.GetComponent<BeatGen>().bpm+=10;
        metronome.GetComponent<BeatGen>().CalculateGranularity();
      }
    }

    public void DecrementBPM() {
      if(globals.GetComponent<Globals>().bpm>60){
        int newValue = globals.GetComponent<Globals>().bpm - 10;
        globals.GetComponent<Globals>().bpm = newValue;
        bpmValue.GetComponent<Text>().text = newValue.ToString();
        metronome.GetComponent<BeatGen>().bpm-=10;
        metronome.GetComponent<BeatGen>().CalculateGranularity();
      }
    }

}
