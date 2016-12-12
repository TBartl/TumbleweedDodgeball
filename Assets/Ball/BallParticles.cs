using UnityEngine;
using System.Collections;

public class BallParticles : MonoBehaviour {

	BallHotness hotness;
	BallSource source;
	public GameObject particlesPrefab;

	void Start() {
		hotness = GetComponent<BallHotness>();
		source = GetComponent<BallSource>();
	}

	void OnCollisionEnter2D(Collision2D coll) {

		if (hotness.GetIsHot()) {
			if (coll.collider.tag == "Player") {
				GameObject particles = Instantiate(particlesPrefab);
				particles.transform.position = transform.position;
				particles.GetComponent<BallParticleSystem>().SetColor(
					PlayerManager.inst.GetColor(source.GetThrower().num));
			}
		}
	}
}
