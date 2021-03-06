﻿using UnityEngine;
using System.Collections;

public class BallHotness : MonoBehaviour {
	TrailRenderer tr;
	protected Rigidbody2D rigid;
	protected BallSource source;

	public float threshold;
	bool isHot = false;

	float initialTrailWidth;
	float initialTrailTime;

	void Awake()
	{
		tr = this.GetComponentInChildren<TrailRenderer>();
		rigid = this.GetComponent<Rigidbody2D>();
		source = this.GetComponent<BallSource>();

		initialTrailWidth = tr.startWidth;
		initialTrailTime = tr.time;
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		if (rigid.velocity.magnitude < threshold) {
			isHot = false;
		}
		if (isHot)
			tr.startWidth = initialTrailWidth;
		else
			tr.startWidth = Mathf.Max(0, tr.startWidth - 2 * Time.deltaTime);//Hardcoded because yolo

		// If we're effectively at 0 velocity, don't record the trail 
		// so when we throw it the trail will start from the hands
		if (rigid.velocity.magnitude <= .05f)
			tr.time = 0;
		else
			tr.time = initialTrailTime;

	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (isHot && 
			(other.gameObject.tag == "Hittable" ||
			(other.gameObject.tag == "Player" && other.gameObject.GetComponent<PlayerHittable>().GetHittable(source.GetThrower())))
			) {
			other.gameObject.GetComponent<Hittable>().Hit(source.GetThrower(), rigid.velocity);
			this.OnHitOther(other.gameObject);
		}
		else {
			HitAnything();
		}
	}
	

	protected virtual void OnHitOther(GameObject other) {

	}

	protected virtual void HitAnything() {

	}

	public bool GetIsHot() {
		return isHot;
	}

	public void makeHot() {
		isHot = true;
	}
}
