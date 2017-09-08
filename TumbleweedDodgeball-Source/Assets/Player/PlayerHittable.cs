using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerHittable : Hittable {
	PlayerData playerData;
	public float invincibilityFrameLength = 1.5f;
	public float flickerInterval = .2f;
	bool hasInvincibilityFrames = false;

	public Renderer playerMesh;

	void Awake() {
		playerData = this.GetComponent<PlayerData>();
	}

	public override void Hit(PlayerData source, Vector2 velocityHit) {
		//check for invincibility 
		if (hasInvincibilityFrames || PowerupManager.S.getPowerup(playerData.num) == Powerup.Invincible) return;

		if (playerData == source)
			return;

		base.Hit(source, velocityHit);
        StartCoroutine(GetComponent<PlayerMovement>().KnockBack(velocityHit));
		StartCoroutine(InvincibilityFrames());
		if (source != null)
			ScoreManager.inst.IncrementScore(source.num, playerData.num);
		ScoreManager.inst.DecrementScore(playerData.num);

		//AudioManager.instance.PlayClipAtPoint(AudioManager.instance.playerHit, transform.position);

	}

	public bool GetHittable(PlayerData source) {
		return (!hasInvincibilityFrames && source != playerData);

	}

	IEnumerator InvincibilityFrames() {
		hasInvincibilityFrames = true;
		TurnOffMesh();

		for (float t = 0; t < invincibilityFrameLength; t += Time.deltaTime) {
			if (Mathf.RoundToInt(t / flickerInterval) % 2 == 0) {
				TurnOffMesh();
			} else {
				TurnOnMesh();
			}

			yield return null;
		}
		TurnOnMesh();
		hasInvincibilityFrames = false;
	}

	public bool HasInvincibility() {
		return hasInvincibilityFrames || PowerupManager.S.getPowerup(playerData.num) == Powerup.Invincible;
	}

	void TurnOnMesh() {
		playerMesh.enabled = true;
	}

	void TurnOffMesh() {
		playerMesh.enabled = false;
	}
}
