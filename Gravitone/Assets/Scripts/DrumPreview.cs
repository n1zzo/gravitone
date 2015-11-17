using UnityEngine;
using System.Collections;

public class DrumPreview : Drum {

	public override void UpdateState() {

		// This is executed at every beat.
		if (lastBeat && currentSlot != lastSlot) {
			if (prev[currentSlot])
				playDrum();
			lastSlot = currentSlot;
			lastBeat = false;
		}
	}
}
