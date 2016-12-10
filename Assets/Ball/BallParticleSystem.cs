using UnityEngine;
using System.Collections;

public class BallParticleSystem : MonoBehaviour {

	public float numFrames;
	Color color = Color.white;
	ParticleSystem particles;

	// Use this for initialization
	void Start () {
		particles = GetComponent<ParticleSystem>();
		StartCoroutine(StartParticles());
	}


	public void SetColor(Color colorIn) {
		color = colorIn;
	}

	IEnumerator StartParticles() {
		particles.startColor = color;
		particles.Play();
		for (int i = 0; i < numFrames; ++i) {
			yield return null;
		}
		particles.Stop();
		StartCoroutine(DestroyParticlesWhenFinished());
	}

	IEnumerator DestroyParticlesWhenFinished() {
		while (particles.particleCount > 0) {
			yield return null;
		}
		Destroy(gameObject);
	}
}
