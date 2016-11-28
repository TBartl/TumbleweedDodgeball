using UnityEngine;
using System.Collections;

public class ExplodeAndRespawnOnHit : Hittable {

	public GameObject shardsPrefab;
	Renderer rend;
	Collider2D coll;
	OnHide onHide;

	bool justDestroyed = false, isDestroyed = false;
	float respawnTime;
	public float durationMin, durationMax;

	void Start() {
		rend = GetComponent<Renderer>();
		coll = GetComponent<Collider2D>();
		onHide = GetComponent<OnHide>();
	}

	void Update() {
		if (justDestroyed) {
			justDestroyed = false;
			respawnTime = Time.time + Random.Range(durationMin, durationMax);
		}
		if (isDestroyed && Time.time >= respawnTime) {
			rend.enabled = true;
			coll.enabled = true;
			isDestroyed = false;
		}
	}

	public override void Hit(PlayerData source, Vector2 velocityHit) {
		base.Hit(source, velocityHit);
		rend.enabled = false;
		coll.enabled = false;
		Instantiate(shardsPrefab, this.transform.position, Quaternion.identity);
		justDestroyed = isDestroyed = true;
		onHide.Hide();
	}
}
