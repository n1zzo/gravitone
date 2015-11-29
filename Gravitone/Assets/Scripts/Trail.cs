using UnityEngine;
using System.Collections;

public class Trail : MonoBehaviour {

	public GameObject star;
	float x,y,MAXX,MAXY, acty,actx;
	BeatGen beats;

	// Use this for initialization
	void Start () {
		x=0;
		MAXX=Camera.main.ScreenToWorldPoint(new Vector3(Screen.width+1, Screen.height, 0)).x - 0.2f;
		MAXY=Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)).y - 0.2f;
		y=MAXY;
	}

	// Update is called once per frame
	void Update () {
		beats=star.GetComponent<BeatGen>();

			if(beats.progress<0.25f){

				x = beats.progress*4*MAXX;
				actx=x;

			}
			else if ( beats.progress<0.5f){

				y = MAXY - (beats.progress - 0.25f) *4*MAXX;
				acty=y;

			}
			else if(beats.progress<0.75f)

				y = acty + (beats.progress - 0.5f)*4*MAXX;

			else if(beats.progress<1f)

				x = actx - (beats.progress % 0.75f)*4*MAXX;


		if(x>0 && y<MAXY)
			transform.position=new Vector3(x,y,-1f);
		else if(x<=0)
			transform.position=new Vector3(0,y,-1f);
		else
			transform.position=new Vector3(x,MAXY,-1f);
	}
}
