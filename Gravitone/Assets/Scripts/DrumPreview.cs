using UnityEngine;
using System.Collections;

public class DrumPreview : Drum {

	void Update() {

		// This is executed at every beat.
		if (lastBeat && currentSlot != lastSlot) {
			if (prev[currentSlot])
				playDrum();
			lastSlot = currentSlot;
			lastBeat = false;
		}
	}
}
