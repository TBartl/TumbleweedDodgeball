using UnityEngine;
using System.Collections;

public class BallHotnessAndDestroy : BallHotness {

	protected override void OnHit() {
		Destroy(this.gameObject);
	}
}
