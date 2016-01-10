using UnityEngine;
using System.Collections;

public class AlphaColor : MonoBehaviour {

	private SpriteRenderer spriteRenderer;
	private float transparency;

	// Use this for initialization
	void Start () {
		spriteRenderer = this.GetComponent<SpriteRenderer>();
	}

	// Update is called once per frame
	void Update () {

	}

	public void SetTransparency(float level){
		transparency = level;
		spriteRenderer.color = new Color(1f,1f,1f,level);
	}

	public void SetColor(float r, float g, float b) {
		spriteRenderer.color = new Color(r, g, b, transparency);
	}

}
