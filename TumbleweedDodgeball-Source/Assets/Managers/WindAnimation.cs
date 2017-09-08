using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WindAnimation : MonoBehaviour {

	public GameObject[] windParticles;
	List<Vector3> startPositions;
	public float numFrames, multiplier;

	void Start() {
		startPositions = new List<Vector3>();
		foreach (GameObject windParticle in windParticles) {
			windParticle.GetComponent<TrailRenderer>().enabled = false;
			startPositions.Add(windParticle.transform.position);
		}
	}

	public void StartAnimation(Vector2 force) {
		StartCoroutine(Animate(force));
	}

	IEnumerator Animate(Vector2 force) {
		for (int i = 0; i < numFrames; ++i) {
			foreach (GameObject windParticle in windParticles) {
				windParticle.GetComponent<TrailRenderer>().enabled = true;
				windParticle.GetComponent<Rigidbody2D>().AddForce(force /** ((float) i / (float) numFrames)*/ * multiplier);
			}
			yield return null;
		}
		for (int i = 0; i < windParticles.Length; ++i) {
			StartCoroutine(DisableTrailOnStop(windParticles[i], i));
		}
	}

	IEnumerator DisableTrailOnStop(GameObject windParticle, int num) {
		Rigidbody2D body = windParticle.GetComponent<Rigidbody2D>();
		while (body.velocity != Vector2.zero) {
			yield return null;
		}
		windParticle.transform.position = startPositions[num];
		windParticle.GetComponent<TrailRenderer>().enabled = false;
	}
}
