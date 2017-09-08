using UnityEngine;
using System.Collections;

public class BallCameraShake : MonoBehaviour {

	BallHotness hotness;
	CameraShake cameraShake;

	void Start() {
		hotness = GetComponent<BallHotness>();
		cameraShake = Camera.main.GetComponent<CameraShake>();
	}
	void OnCollisionEnter2D(Collision2D coll) {

		if (hotness.GetIsHot() && coll.collider.tag == "Player") {
			cameraShake.StartShake();
		}
	}
}
