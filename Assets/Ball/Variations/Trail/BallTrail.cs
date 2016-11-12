using UnityEngine;
using System.Collections;

public class BallTrail : MonoBehaviour {

	BallHotness hotness;
	bool hot = false;
	ParticleSystem trail;
	EdgeCollider2D trailCollider;
	Vector3 prevPosition;

	// Use this for initialization
	void Start () {
		hotness = GetComponentInParent<BallHotness>();
		trail = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
		if (hotness.GetIsHot() != hot) {
			hot = hotness.GetIsHot();
			if (hot) {
				trail.Play();
			}
			else {
				trail.Stop();
			}
		}
	}
}
