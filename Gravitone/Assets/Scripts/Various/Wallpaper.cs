using UnityEngine;
using System.Collections;

public class Wallpaper : MonoBehaviour {

	float movementx=0;
	float movementy=0;
	float speedx=0.00012f;
	float speedy=0.00007f;
	bool forwardx=true;
	bool forwardy=true;

	// Use this for initialization
	void Start () {
        //Adjust the wallpaper according to screen height
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        float worldScreenHeight = Camera.main.orthographicSize * 2 ;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        transform.localScale = new Vector3(
            worldScreenHeight / sr.sprite.bounds.size.y,
            worldScreenHeight / sr.sprite.bounds.size.y, 1);
	}

	// Update is called once per frame
	void Update () {
		if(forwardx){
			transform.localScale += new Vector3 (speedx, 0, 0);
			movementx+=speedx;
			if(movementx>0.1f)
				forwardx=false;
		} else {
			transform.localScale -= new Vector3 (speedx, 0, 0);
			movementx-=speedx;
			if(movementx<0)
				forwardx=true;
		}

		if(forwardy){
			transform.localScale += new Vector3 (0, speedy, 0);
			movementy+=speedy;
			if(movementy>0.1f)
				forwardy=false;
		} else {
			transform.localScale -= new Vector3 (0, speedy, 0);
			movementy-=speedy;
			if(movementy<0)
				forwardy=true;
		}
	}
}
