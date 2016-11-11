using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HomingBall : MonoBehaviour {

	float speed;
	Rigidbody2D body;
	int source;
	BallSource ballSource;
    GameObject playerAimingFor = null;

    void Start() {
        ballSource = GetComponent<BallSource>();
        body = GetComponent<Rigidbody2D>();
    }

    void Update() {
        if (ballSource.GetSourceID() != source && playerAimingFor == null) {
            source = ballSource.GetSourceID();
            if (source != -1) { // ball was just thrown
                speed = body.velocity.magnitude;
                //find closest player to fire upon
                playerAimingFor = findClosestPlayer();
                print(playerAimingFor);
            }
        }
        else if (playerAimingFor != null) {
            //FIXME THIS FAILS AT TIMES
            body.velocity = (playerAimingFor.transform.position - transform.position).normalized * speed;
        }
        
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.collider.tag == "Player") {
            Destroy(this.gameObject); //FIXME what happens on hit?
		}
	}

    GameObject findClosestPlayer() {
        GameObject closestPlayer = null;
        float shortestDistance = float.MaxValue, distance;
        foreach (GameObject player in PlayerManager.inst.players) {
            distance = distanceToPlayer(player);
            if (closestPlayer == null || distance < shortestDistance) {
                if (source == player.GetComponent<PlayerData>().num) continue;
                closestPlayer = player;
                shortestDistance = distance;
            }
        }
        return closestPlayer;
    }

	float distanceToPlayer(GameObject player) {
		return Vector3.Distance(transform.position, player.transform.position);
	}
}
