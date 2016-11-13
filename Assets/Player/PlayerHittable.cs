using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerHittable : Hittable {
	PlayerData playerData;
	public float invincibilityFrameLength = 1.5f;
	public float flickerInterval = .2f;
	bool hasInvincibilityFrames = false;

	List<MeshRenderer> meshes;

	void Awake() {
		playerData = this.GetComponent<PlayerData>();
		meshes = new List<MeshRenderer>(this.GetComponentsInChildren<MeshRenderer>());
	}

	public override void Hit(PlayerData source) {
		//check for invincibility 
		if (hasInvincibilityFrames)
			return;

		if (playerData == source)
			return;

		base.Hit(source);
		StartCoroutine(InvincibilityFrames());
		ScoreManager.inst.IncrementScore(source.num);
		ScoreManager.inst.DecrementScore(playerData.num);

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
		foreach (MeshRenderer mr in meshes) {
			mr.enabled = true;
		}
	}

	void TurnOffMesh() {
		foreach (MeshRenderer mr in meshes) {
			mr.enabled = false;
		}
	}
}
