using UnityEngine;
using System.Collections;

public class PlayerHittable : Hittable {
	PlayerData playerData;

	void Awake() {
		playerData = this.GetComponent<PlayerData>();
	}

	public override void Hit(int source) {
		base.Hit(source);

		if (playerData.num == source)
			return;

		ScoreManager.inst.IncrementScore(source);
	}
}
