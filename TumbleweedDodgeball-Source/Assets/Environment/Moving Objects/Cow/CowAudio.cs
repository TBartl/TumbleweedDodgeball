using UnityEngine;
using System.Collections;

public class CowAudio : MonoBehaviour {

	public float timeBetweenMin, timeBetweenMax;
	float nextMooTime;

	// Use this for initialization
	void Start () {
		GenerateNextMooTime();
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time >= nextMooTime) {
			AudioManager.instance.PlayClipAtPoint(AudioManager.instance.moo, transform.position);
			GenerateNextMooTime();
		}
	}

	void GenerateNextMooTime() {
		nextMooTime = Time.time + Random.Range(timeBetweenMin, timeBetweenMax);
	}
}
