using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BallTrailCollider : MonoBehaviour {

	BallHotness hotness;
	Vector2 prevPosition;
	List<Vector2> vertices;
	ParticleSystem trail;
	ParticleSystem.Particle[] particles;
	List<PlayerMovement> playerMovements;
	BallSource ballSource;

	void Start() {
		trail = GetComponent<ParticleSystem>();
		ballSource = GetComponentInParent<BallSource>();
		particles = new ParticleSystem.Particle[trail.maxParticles];
	}

	void Update() {
		int numParticles = trail.GetParticles(particles);
		for (int i = 0; i < numParticles; i += 5) {
			CheckForCollision(particles[i]);
		}
	}

	void CheckForCollision(ParticleSystem.Particle particle) {
		foreach (GameObject player in PlayerManager.inst.players) {
			if (player.GetComponent<PlayerData>() != ballSource.GetThrower()) {
				if (Vector2.Distance(player.transform.position, particle.position) < 1) {
					player.GetComponent<PlayerDaze>().InBallTrail();
				}
			}
		}
	}
}
