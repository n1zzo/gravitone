using UnityEngine;
using System.Collections;

public class DrumPlay : Drum {

	public override void UpdateState() {

		if (checkFire()) {
			playDrum();
		}

		// This is executed at every beat.
		if (lastBeat && currentSlot != lastSlot) {
			if (slots[currentSlot])
				playDrum();
			lastSlot=currentSlot;
			lastBeat = false;
		}
	}
}
