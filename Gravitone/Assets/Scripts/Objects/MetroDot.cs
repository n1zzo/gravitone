using UnityEngine;
using System.Collections;

public class MetroDot : MonoBehaviour {

	public Object pointPrefab;
	private GameObject[] points = new GameObject[64];
	private float radius;
	private float dotOffset = 0.1f;
    private int totalDots = 0;
    public int phase = 0;

	// Instantiate the dots into the scene at the correct position
	public void PlaceDots(int beatsPerBar, int subBeatsPerBeat, float radius) {
		int count = 0;
		float angleQuantum = (Mathf.PI * 2) / beatsPerBar;
		float offsetQuantum = Mathf.PI / (3 * subBeatsPerBeat);
		float currentAngle = 0;
		float currentOffset = 0;
		for (int i = 0; i < beatsPerBar; i++) {
			currentOffset = - ((offsetQuantum * subBeatsPerBeat) / 2);
				for (int j = 0; j < subBeatsPerBeat; j++) {
					float x = radius * Mathf.Sin(currentAngle + currentOffset);
					float y = radius * Mathf.Cos(currentAngle + currentOffset);
					points[count] = (GameObject)Instantiate(pointPrefab, new Vector3(x, y, 0), Quaternion.identity);
					count++;
					currentOffset += offsetQuantum;
				}
				currentAngle += angleQuantum;
		}
        totalDots = count;
	}

	public void FillDot(int number) {
		GameObject fullDot = points[number].GetComponent<DotGroup>().fullDot;
		GameObject emptyDot = points[number].GetComponent<DotGroup>().emptyDot;
		fullDot.GetComponent<DotManager>().Fill();
	}

	public void ColorDot(int number) {
		GameObject fullDot = points[number].GetComponent<DotGroup>().fullDot;
        switch(phase) {
            case 0:
                fullDot.GetComponent<DotManager>().MakePink();
                break;
            case 1:
                fullDot.GetComponent<DotManager>().MakeYellow();
                break;
            case 2:
                fullDot.GetComponent<DotManager>().MakeBlue();
                break;
            default:
                fullDot.GetComponent<DotManager>().MakePink();
                break;
        }
	}

    public void HitDot(int number) {
		GameObject fullDot = points[number].GetComponent<DotGroup>().fullDot;
		fullDot.GetComponent<DotManager>().Hit();
	}

    public void ResetDot() {
        // Resets all the dots
        for(int i=0; i < totalDots; i++) {
            GameObject fullDot = points[i].GetComponent<DotGroup>().fullDot;
            fullDot.GetComponent<DotManager>().Reset();
        }
	}

    public void ResetPink() {
        // Resets all the dots
        for(int i=0; i < totalDots; i++) {
            GameObject fullDot = points[i].GetComponent<DotGroup>().fullDot;
            fullDot.GetComponent<DotManager>().ResetPink();
        }
    }

    public void DestroyAll() {
        for(int i=0; i < totalDots; i++) {
            Destroy(points[i].GetComponent<DotGroup>().fullDot);
            Destroy(points[i].GetComponent<DotGroup>().emptyDot);
            Destroy(points[i]);
        }
    }

    public void SwitchColor() {
        phase++;
    }
}
