using UnityEngine;
using System.Collections;

public class BallTrail : MonoBehaviour {

	BallHotness hotness;
	bool hot = false;
	ParticleSystem trail;
	GameObject trailObject;
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
					trailObject.transform.position = new Vector3(transform.position.x, transform.position.y, -1);
					trail = trailObject.GetComponent<ParticleSystem>();
					trailObject.GetComponent<BallTrailCollider>().SetBallSource(ballSource);
				}
				else {
					if (trailObject != null) {
						trail.Stop();
					}
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
