using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	public bool online = true;
	public GameObject OnlineMetronome;
	public GameObject OfflineMetronome;
	GameObject currentMetronome;

	// Use this for initialization
	void Start () {
		if(online) {
			currentMetronome = OnlineMetronome;
		}
		else {
			currentMetronome = OfflineMetronome;
		}
	}

	public GameObject GetMetronome () {
		return currentMetronome;
	}

}
