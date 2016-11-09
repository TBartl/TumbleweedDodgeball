using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BallCombo : MonoBehaviour {

	float speed;
	Rigidbody2D body;
	List<int> playersVisited;
	int source;
	BallSource ballSource;

	// Use this for initialization
	void Start() {
		playersVisited = new List<int>();
		ballSource = GetComponent<BallSource>();
		body = GetComponent<Rigidbody2D>();
	}

	void Update() {
		if (ballSource.GetSourceID() != source) {
			source = ballSource.GetSourceID();
			playersVisited.Clear();
			if (source != -1) { // ball was just thrown
				playersVisited.Add(source);
				speed = body.velocity.magnitude;
			}
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.collider.tag == "Player") {
			playersVisited.Add(coll.collider.gameObject.GetComponent<PlayerData>().num);

			GameObject nextToVisit = null;
			float shortestDistance = float.MaxValue, distance;
			foreach (GameObject player in PlayerManager.inst.players) {
				if (!playersVisited.Contains(player.GetComponent<PlayerData>().num)) {
					distance = distanceToPlayer(player);
					if (nextToVisit == null || distance < shortestDistance) {
						nextToVisit = player;
						shortestDistance = distance;
					}
				}
			}

			if (nextToVisit != null) {

				body.velocity = (transform.position - nextToVisit.transform.position).normalized * speed;
			}
		}
	}

	float distanceToPlayer(GameObject player) {
		return Vector3.Distance(transform.position, player.transform.position);
	}
}
