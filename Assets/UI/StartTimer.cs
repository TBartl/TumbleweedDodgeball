using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class StartTimer : MonoBehaviour {

	private Image image;
	public List<Sprite> images;
	Vector3 overridePosition;
	bool started = false;
	public AnimationCurve curve;

	void Start () {
		image = GetComponent<Image>();
		StartCoroutine(CountDown());
	}

	IEnumerator CountDown () {
		started = true; // Let the camera get into its final starting position
		yield return null;

		image.enabled = true;
		float prev = 4;
		
		List<Vector3> cameraPositions = new List<Vector3>();
		cameraPositions.Add(Camera.main.transform.position);
		started = false;
		foreach (GameObject go in PlayerManager.inst.players) {
			if (go.activeSelf)
				cameraPositions.Add(go.transform.position + new Vector3(0, -1f, -3.5f));
		}
		cameraPositions.Add(Camera.main.transform.position);

		for (float t = 3; t > 0; t -= Time.deltaTime) {
			image.sprite = images[Mathf.CeilToInt(t)];
			if (Mathf.Ceil(t) != prev) {
				AudioManager.instance.PlayClip(AudioManager.instance.timer);
				prev = Mathf.Ceil(t);
			}

			float floatIndex = t / 3.0f;
			floatIndex *= cameraPositions.Count;
			float smoothedPercent = floatIndex % 1f;
			smoothedPercent = curve.Evaluate(smoothedPercent);

			overridePosition = Vector3.Lerp(
				cameraPositions[Mathf.Clamp(Mathf.FloorToInt(floatIndex), 0, cameraPositions.Count - 1)],
				cameraPositions[Mathf.Clamp(Mathf.CeilToInt(floatIndex),  0, cameraPositions.Count - 1)],
				smoothedPercent);
			yield return null;
		}

        PlayerManager.inst.UnfreezePlayers();
		image.sprite = images[0];
		started = true;

		AudioManager.instance.PlayClip(AudioManager.instance.timer);
		AudioManager.instance.StartMusic();
		for (float t = 0; t < 1f; t += Time.deltaTime)
			yield return null;

		image.enabled = false;
		GameTimer.play = true;
	}

	void LateUpdate() {
		if (!started)
			Camera.main.transform.position = overridePosition;
	}

}
