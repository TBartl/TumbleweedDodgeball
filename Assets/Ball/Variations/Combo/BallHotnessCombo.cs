using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BallHotnessCombo : BallHotness {
	public float speedBoost = 10f;
	List<PlayerData> alreadyHit = new List<PlayerData>();

	protected override void OnHitOther(GameObject other) {
		if (other.tag == "Player") {
			PlayerData otherData = other.GetComponent<PlayerData>();

			// If we hit ourselves bounce off without doing damage
			if (source.GetThrower() == otherData) {

			}

			// If we hit something that's already been hit, destroy the ball
			else if (alreadyHit.Contains(otherData)) {
				Destroy(this.gameObject);
			}

			//If we hit something that hasn't been hit yet, add it to the list
			else {
				alreadyHit.Add(otherData);
				
				// If we've hit everyone we can destroy this
				if (alreadyHit.Count == PlayerManager.inst.GetNumActivePlayers() - 1)
					Destroy(this.gameObject);
			}

		} else {
			// Actually not going to destroy this here
			// Can combo off destructible terrain because why not
		}

		// Find a new direction (doesn't matter if this was destroyed)
		GameObject target = FindClosestPlayer();
		if (target != null)
			rigid.velocity = (target.transform.position - this.transform.position).normalized * (rigid.velocity.magnitude + speedBoost);
	}

	GameObject FindClosestPlayer() {
		GameObject closestPlayer = null;
		float shortestDistance = float.MaxValue;
		foreach (GameObject player in PlayerManager.inst.players) {
			float thisDistance = DistanceToPlayer(player);
			if (closestPlayer == null || thisDistance < shortestDistance) {
				if (source.GetThrower() == player.GetComponent<PlayerData>())
					continue;
				if (alreadyHit.Contains(player.GetComponent<PlayerData>()))
					continue;
				if (!player.activeSelf) {
					continue;
				}
				closestPlayer = player;
				shortestDistance = thisDistance;
			}
		}
		return closestPlayer;
	}

	float DistanceToPlayer(GameObject player) {
		return Vector3.Distance(transform.position, player.transform.position);
	}

}
