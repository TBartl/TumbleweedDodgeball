using UnityEngine;
using System.Collections;

public class BallHotnessAndDestroy : BallHotness {

	protected override void OnHit(string tag) {
		Destroy(this.gameObject);
	}
}
