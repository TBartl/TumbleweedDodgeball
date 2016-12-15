using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// TODO: Modify this to use 
public class BallHoming : MonoBehaviour {

	public float aggressiveness;
	public float seekThreshold = 5f;

	Rigidbody2D rigid;
	BallSource ballSource;
	BallHotness hotness;
	
	void Awake() {
		rigid = this.GetComponent<Rigidbody2D>();
		ballSource = this.GetComponent<BallSource>();
		hotness = GetComponent<BallHotness>();
	}

    void FixedUpdate() {
		if (hotness.GetIsHot() && rigid.velocity.magnitude > seekThreshold) {
			GameObject target = FindClosestPlayer();

			Vector3 currentDirection = rigid.velocity.normalized;
			Vector3 targetDirection = (target.transform.position - this.transform.position).normalized;
			Vector3 newDirection = Vector3.Slerp(currentDirection, targetDirection, Time.deltaTime * aggressiveness);

			rigid.velocity = newDirection * rigid.velocity.magnitude;
		}
	}

    GameObject FindClosestPlayer() {
        GameObject closestPlayer = null;
		float shortestDistance = float.MaxValue;
        foreach (GameObject player in PlayerManager.inst.players) {
			if (player.activeSelf) {
				float thisDistance = DistanceToPlayer(player);
				if (closestPlayer == null || thisDistance < shortestDistance) {
					if (ballSource.GetThrower() == player.GetComponent<PlayerData>())
						continue;
					closestPlayer = player;
					shortestDistance = thisDistance;
				}
			}
        }
        return closestPlayer;
    }
		
	float DistanceToPlayer(GameObject player) {
		return Vector3.Distance(transform.position, player.transform.position);
	}
}
