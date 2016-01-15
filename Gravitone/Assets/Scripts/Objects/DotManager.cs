using UnityEngine;
using System.Collections;

public class DotManager : MonoBehaviour {

	public float fadeSpeed = 2f;
	public float alphaTreshold = 0.2f;
	private float transparency = 0f;
	private bool isPink = false;
	private Vector3 originalScale;

	// Use this for initialization
	void Start () {
		originalScale=transform.localScale;
	}

	// Update is called once per frame
	void Update () {
		// Decrease alpha channel until treshold is reached
		if(transparency > alphaTreshold) {
			float decrement = fadeSpeed*Time.deltaTime;
			transparency -= decrement;
			this.GetComponent<AlphaColor>().SetTransparency(transparency);
		}
		else {
			transparency = alphaTreshold;
		}

		if(originalScale.x < transform.localScale.x)
			transform.localScale -= new Vector3(0.02f, 0.02f, 0);
	}

	public void Fill() {
		this.GetComponent<AlphaColor>().SetTransparency(1f);
		transparency = 1f;
	}

	public void MakePink() {
		if(!isPink) {
			this.GetComponent<AlphaColor>().SetColor(0.95f, 0.57f, 0.72f);
			transform.localScale += new Vector3(0.8f, 0.8f, 0);
			originalScale=transform.localScale;
			alphaTreshold = 0.4f;
			isPink = true;
		}
	}

	public void MakeGreen(){
		this.GetComponent<AlphaColor>().SetColor(0f, 1f, 0f);
		transform.localScale += new Vector3(0.2f, 0.2f, 0);
	}

	public void MakeRed(){
		this.GetComponent<AlphaColor>().SetColor(1f, 0f, 0f);
		transform.localScale += new Vector3(0.8f, 0.8f, 0);
	}

	public void Reset(){
		if(!isPink)
			this.GetComponent<AlphaColor>().SetColor(1f, 1f, 1f);
		else
			this.GetComponent<AlphaColor>().SetColor(0.95f, 0.57f, 0.72f);
	}

	public void ResetPink(){
		if(isPink){
			transform.localScale -= new Vector3(0.8f, 0.8f, 0);
			originalScale=transform.localScale;
			isPink=false;
			alphaTreshold=0;
		}

		this.GetComponent<AlphaColor>().SetColor(1f, 1f, 1f);
	}

	public void Hit(){
		if(isPink)
            MakeGreen();
        else
            MakeRed();
	}
}
