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
		acty=y;
		actx=x;
		transform.position=new Vector3(0,MAXY,-1f);
	}

	// Update is called once per frame
	void Update () {

		float progress= Mathf.Round(star.GetComponent<BeatGen>().progress*100)/100;

			if(progress<0.25f){

				x = progress*4*MAXX;
				actx=x;

			}
			else if (progress<0.5f){

				y = MAXY - (progress - 0.25f) *4*MAXX;
				acty=y;

			}
			else if(progress<0.75f)

				y = acty + (progress - 0.5f)*4*MAXX;

			else if(progress<1f)

				x = actx - (progress % 0.75f)*4*MAXX;


		if(x>0 && y<MAXY)
			transform.position=new Vector3(x,y,-1f);
		else if(x<=0)
			transform.position=new Vector3(0,y,-1f);
		else
			transform.position=new Vector3(x,MAXY,-1f);
	}

	public void SetInitialPosition(){
				acty=MAXY;
				actx=0;
				y=MAXY;
				x=0;
				transform.position=new Vector3(0,MAXY,-1f);
	}

	public void SetInitialY(){
				acty=MAXY;
				y=MAXY;
				transform.position=new Vector3(transform.position.x,MAXY,-1f);
	}
}
