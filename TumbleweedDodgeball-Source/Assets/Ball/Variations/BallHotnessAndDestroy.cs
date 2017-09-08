using UnityEngine;
using System.Collections;

public class BallHotnessAndDestroy : BallHotness {

	protected override void OnHitOther(GameObject other) {
		Destroy(this.gameObject);
	}
}
