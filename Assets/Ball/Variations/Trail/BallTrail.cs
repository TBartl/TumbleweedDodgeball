﻿using UnityEngine;
using System.Collections;

public class BallTrail : MonoBehaviour {

	BallHotness hotness;
	bool hot = false;
	ParticleSystem trail;
	GameObject trailObject;
	EdgeCollider2D trailCollider;
	Vector3 prevPosition;
	BallSource ballSource;

	public GameObject trailPrefab;

	// Use this for initialization
	void Start () {
		hotness = GetComponent<BallHotness>();
		ballSource = GetComponent<BallSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if (hotness != null) {
			if (hotness.GetIsHot() && trailObject != null) {
				trailObject.transform.position = new Vector3(transform.position.x, transform.position.y, -1);
			}
			if (hotness.GetIsHot() != hot) {
				hot = hotness.GetIsHot();
				if (hot) {
					trailObject = Instantiate(trailPrefab);
					trailObject.transform.position = transform.position;
					trail = trailObject.GetComponent<ParticleSystem>();
					trailObject.GetComponent<BallTrailCollider>().SetBallSource(ballSource);
				}
				else {
					trail.Stop();
				}
			}
		}
	}

	void OnDestroy() {
		if (trail != null) {
			trail.Stop();
		}
	}
}
