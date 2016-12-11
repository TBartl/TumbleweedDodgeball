using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {

	public int numFrames;
	public float shakeIntensity;
	public int framesBetweenShake;

	// Use this for initialization
	void Start () {
	
	}
	
	public void StartShake() {
		StartCoroutine(Shake(shakeIntensity));
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
