using UnityEngine;
using System.Collections;

public class PlayerHittable : Hittable {
	PlayerData playerData;

	void Awake() {
		playerData = this.GetComponent<PlayerData>();
	}

	public override void Hit(int source) {
        //check for invincibility powerup
        if (PowerupManager.S.getPowerup(playerData.num) == Powerup.Invincible) return;

		base.Hit(source);

		if (playerData.num == source)
			return;

		ScoreManager.inst.IncrementScore(source);
	}
}
