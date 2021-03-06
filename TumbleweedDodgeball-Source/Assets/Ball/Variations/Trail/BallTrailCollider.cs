﻿using UnityEngine;
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
		particles = new ParticleSystem.Particle[trail.maxParticles];
	}

	void Update() {
		int numParticles = trail.GetParticles(particles);
		if (numParticles == 0) {
			Destroy(gameObject);
		}
		for (int i = 0; i < numParticles; ++i) {
			CheckForCollision(particles[i]);
		}
	}

	void CheckForCollision(ParticleSystem.Particle particle) {
		foreach (GameObject player in PlayerManager.inst.players) {
			if (player.activeSelf && player.GetComponent<PlayerData>() != ballSource.GetThrower()) {
				if (particle.remainingLifetime >= 2.5 && Vector2.Distance(player.transform.position, particle.position) < .5) {
					player.GetComponent<PlayerDaze>().Daze();
				}
			}
		}
	}

	public void SetBallSource(BallSource ballSourceIn) {
		ballSource = ballSourceIn;
	}
}
