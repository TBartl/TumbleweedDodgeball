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

	void Start() {
		trail = GetComponent<ParticleSystem>();
		particles = new ParticleSystem.Particle[trail.maxParticles];
		//foreach ()
	}

	void Update() {
		int numParticles = trail.GetParticles(particles);
		for (int i = 0; i < numParticles; i += 5) {
			CheckForCollision(particles[i]);
		}
	}

	void CheckForCollision(ParticleSystem.Particle particle) {
		foreach (GameObject player in PlayerManager.inst.players) {
			if (Physics2D.Raycast(particle.position, player.transform.position - particle.position, 1f)) {
				//player.GetComponent<PlayerMovement>().modifiers.Add(.5f);
				//player.
			}
		}
	}
}
