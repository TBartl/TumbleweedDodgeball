using UnityEngine;
using System.Collections;

public class TrainAudio : MonoBehaviour {

	public float timeBetweenMin, timeBetweenMax;
	float nextWhistleTime;

	// Use this for initialization
	void Start() {
		GenerateNextWhistleTime();
	}

	// Update is called once per frame
	void Update() {
		if (Time.time >= nextWhistleTime) {
			print("here");
			AudioManager.instance.PlayClipAtPoint(AudioManager.instance.trainWhistle, transform.position);
			GenerateNextWhistleTime();
		}
	}

	void GenerateNextWhistleTime() {
		nextWhistleTime = Time.time + Random.Range(timeBetweenMin, timeBetweenMax);
	}
}
