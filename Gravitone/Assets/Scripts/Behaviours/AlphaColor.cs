using UnityEngine;
using System.Collections;

public class AlphaColor : MonoBehaviour {

	private SpriteRenderer spriteRenderer;
	private float transparency;
	private float r;
	private float g;
	private float b;
	private float baseAlpha = 0.6f;


	// Use this for initialization
	void Start () {
		spriteRenderer = this.GetComponent<SpriteRenderer>();
		// Get original color values and transparency
		r = spriteRenderer.color.r;
		g = spriteRenderer.color.g;
		b = spriteRenderer.color.b;
		transparency = spriteRenderer.color.a;
		// Set initial transparency
		SetTransparency(baseAlpha);
	}

	public void SetTransparency(float level){
		transparency = level;
		spriteRenderer.color = new Color(r, g, b, level);
	}

	public void SetColor(float r, float g, float b) {
		this.r = r;
		this.g = g;
		this.b = b;
		spriteRenderer.color = new Color(r, g, b, transparency);
	}

}
