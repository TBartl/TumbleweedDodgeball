using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WindManager : MonoBehaviour {

	public static WindManager instance;
	List<Rigidbody2D> balls;
	public float timeBetweenMin, timeBetweenMax;
	float nextWindTime;
	float nextWindAnimationTime;
	public Vector2 forceMin, forceMax;
	public int numFrames;
	WindAnimation animation;
	Vector2 force;
	bool animatedWind = false, appliedForce = false;
	public float animationPreDelay;

	void Awake() {
		balls = new List<Rigidbody2D>();
		instance = this;
		animation = GetComponentInChildren<WindAnimation>();
	}

	void Start() {
		GenerateWind();
	}
	
	void Update() {
		if (!animatedWind && Time.time >= nextWindAnimationTime) {
			animation.StartAnimation(force);
			animatedWind = true;
			AudioManager.instance.PlayClip(AudioManager.instance.wind);
		}
		if (!appliedForce && Time.time >= nextWindTime) {
			StartCoroutine(ApplyForce());
		}
	}

	public void AddBall(Rigidbody2D toAdd) {
		balls.Add(toAdd);
	}

	public void RemoveBall(Rigidbody2D toRemove) {
		balls.Remove(toRemove);
	}

	void GenerateWind() {
		nextWindTime = Time.time + Random.Range(timeBetweenMin, timeBetweenMax);
		nextWindAnimationTime = nextWindTime - animationPreDelay;
		force = new Vector2(Random.Range(forceMin.x, forceMax.x), Random.Range(forceMin.y, forceMax.y));
		animatedWind = appliedForce = false;
	}

	IEnumerator ApplyForce() {
		appliedForce = true;
		for (int i = 0; i < numFrames; ++i) {
			foreach (Rigidbody2D body in balls) {
				body.AddForce(force * ((float) i / (float) numFrames));
			}
			yield return null;
		}
		GenerateWind();
	}
}
