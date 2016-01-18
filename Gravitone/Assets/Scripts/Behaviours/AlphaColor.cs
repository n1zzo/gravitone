using UnityEngine;
using System.Collections;

public class AlphaColor : MonoBehaviour {

	private SpriteRenderer spriteRenderer;
	private float transparency;
	private float r;
	private float g;
	private float b;
	public float baseAlpha = 0.8f;


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

    public void DecreaseTransparency(float level) {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        transparency -= level;
		spriteRenderer.color = new Color(r, g, b, transparency);
    }

    public bool isInvisible() {
        return transparency < 0;
    }

	public void SetColor(float rr, float gg, float bb) {
		this.r = rr;
		this.g = gg;
		this.b = bb;
		spriteRenderer.color = new Color(r, g, b, transparency);
	}

}
