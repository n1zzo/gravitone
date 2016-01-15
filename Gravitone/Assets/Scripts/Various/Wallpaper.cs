using UnityEngine;
using System.Collections;

public class Wallpaper : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //Adjust the wallpaper according to screen height
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        float worldScreenHeight = Camera.main.orthographicSize * 2;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;
         
        transform.localScale = new Vector3(
            worldScreenHeight / sr.sprite.bounds.size.y,
            worldScreenHeight / sr.sprite.bounds.size.y, 1);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
