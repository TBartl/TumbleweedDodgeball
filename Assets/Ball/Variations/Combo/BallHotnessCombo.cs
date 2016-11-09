using UnityEngine;
using System.Collections;

public class BallHotnessCombo : BallHotness {
	protected override void OnHit(string tag) {
		if (tag != "Player") {
			Destroy(this.gameObject);
		}
	}
}
