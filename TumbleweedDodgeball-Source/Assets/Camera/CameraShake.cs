using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {

	public int numFrames;
	public float shakeIntensity;
	public int framesBetweenShake;

	public static CameraShake S;

	// Use this for initialization
	void Awake () {
		S = this;
	
	}

	public void StartShake(float dampen = 1) {
		StartCoroutine(Shake(shakeIntensity * dampen));
	}

	IEnumerator Shake(float shakeAmount) {
		Vector3 startPos = transform.position;
		for (int i = 0; i < numFrames; i += framesBetweenShake) {
			transform.localPosition += Random.insideUnitSphere * shakeAmount;
			yield return null;
		}
		transform.localPosition = startPos;
	}
}
