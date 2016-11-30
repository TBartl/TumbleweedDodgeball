using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BallBoomerang  : BallHotness {

	public float speedBoost = -10f;
	GameObject thrower = null;
	private float maxDistance = 10f;

	bool returning = false;

	void Update() {
		if (thrower == null) thrower = FindPlayer();
		if (returning) {
			rigid.velocity = (thrower.transform.position - this.transform.position).normalized * (rigid.velocity.magnitude + speedBoost);
		} else {
			if (thrower == null) thrower = FindPlayer();
			if (thrower != null && Mathf.Abs(Vector2.Distance(thrower.transform.position, this.transform.position)) > maxDistance) returning = true; 
		}
	}

	void OnCollisionEnter2D(Collision2D other) {	
		if (thrower != null && !returning) returning = true;
		else if (thrower != null) Destroy(this.gameObject);
	}

	protected override void OnHitOther(GameObject other) {
		// reverse direction if it wasnt returning so it returns
		if (!returning) {
			returning = true;
			thrower = FindPlayer();
			if (thrower != null)
				rigid.velocity = (thrower.transform.position - this.transform.position).normalized * (rigid.velocity.magnitude + speedBoost);
		} else Destroy(this.gameObject);
	}

	GameObject FindPlayer() {
		GameObject closestPlayer = null;
		foreach (GameObject player in PlayerManager.inst.players) {
			if (source.GetThrower() == player.GetComponent<PlayerData>()) {
				closestPlayer = player;
			}
		}
		return closestPlayer;
	}
}
