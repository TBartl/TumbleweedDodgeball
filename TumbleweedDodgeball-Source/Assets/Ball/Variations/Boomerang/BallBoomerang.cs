﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BallBoomerang : Ball {

	//public BallHotness hotness;

	protected Rigidbody2D rigid;
	protected BallSource source;

	private float speedBoost = 18f;
	GameObject thrower = null;
	private float maxDistance = 40f;

	bool returning = false;

	void Start() {
		//ball = this.GetComponent<Ball>();
		//hotness = this.GetComponent<BallHotness>();
		//rigid = this.GetComponent<Rigidbody2D>();
		//source = this.GetComponent<BallSource>();
	}

	void Update() {
		if (thrower == null) thrower = FindPlayer();
        if (source.GetThrower() != null && !hotness.GetIsHot()) source.SetThrower(null);
		if (returning) {
			rigid.velocity = (thrower.transform.position - this.transform.position).normalized * (speedBoost);
			if (Mathf.Abs(Vector3.Distance(thrower.transform.position, this.transform.position)) < 1f) Destroy(this.gameObject);
		} else {
			if (thrower == null) thrower = FindPlayer();
			if (thrower != null && Mathf.Abs(Vector2.Distance(thrower.transform.position, this.transform.position)) > maxDistance) returning = true; 
		}
	}

	void OnCollisionEnter2D(Collision2D other) {
		//if (thrower != null && !returning) {
		//	returning = true;
		//}
		//else if (thrower != null) {
		//	Destroy(this.gameObject);
		//}
		// reverse direction if it wasnt returning so it returns
		if (!returning) {
			returning = true;
			thrower = FindPlayer();
			if (thrower != null) {
				rigid.velocity = (thrower.transform.position - this.transform.position).normalized * (rigid.velocity.magnitude + speedBoost);
			}
		}
		else {
			Destroy(this.gameObject);
		}
	}

	void OnHitOther(GameObject other) {
		// reverse direction if it wasnt returning so it returns
		if (!returning) {
			returning = true;
			thrower = FindPlayer();
			if (thrower != null) {
				rigid.velocity = (thrower.transform.position - this.transform.position).normalized * (rigid.velocity.magnitude + speedBoost);
			}
		}
		else {
			Destroy(this.gameObject);
		}
	}

	//public void OnTriggerEnter2D(Collider2D coll) {
	//	if (hotness.GetIsHot() && GetIsSmackable() && coll.tag == "Smack") {
	//		thrower = coll.gameObject.transform.parent.transform.parent.gameObject;
	//		returning = false;
	//	}
	//}

	GameObject FindPlayer() {
		GameObject Player = null;
		foreach (GameObject player in PlayerManager.inst.players) {
			if (source.GetThrower() == player.GetComponent<PlayerData>()) {
				Player = player;
			}
		}
		return Player;
	}
}
