using UnityEngine;
using System.Collections;

public class DrumRecord : Drum {

	void Update() {

		if (checkFire()) {

			var index = Mathf.RoundToInt(progress * (float) granularity);

			// If it's divided in N, then the Nth beat is the initial 0
			if(index == granularity)
				index = 0;

			/*The instant sound feedback will be received neither
				when the slot is already occupied nor when the sound
				is quantified afterwards (to avoid double sounds)*/
			if(!slots[index] && index == (int)(progress * (float) granularity)) {
				transform.localScale = new Vector3(1, 1, 1);
				playDrum();
			}

			slots[index] = true;
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
