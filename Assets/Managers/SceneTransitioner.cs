using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransitioner : MonoBehaviour {

	public static SceneTransitioner instance;

	public Image panel;
	public int numFrames;
	public float cameraOffset;

	public Transform cameraTransform;
	public bool doCameraPan;

	void Awake() {
		Controller.Lock();
		instance = this;
		cameraTransform = Camera.main.transform;
		StartCoroutine(FadeOut());
	}

	IEnumerator FadeIn(string scene) {
		Controller.Lock();
		Vector3 originalPosition = cameraTransform.position;
		Vector3 finalPosition = new Vector3(originalPosition.x + cameraOffset, originalPosition.y, originalPosition.z);
		for (int i = 0; i < numFrames; ++i) {
			panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, (float) i / (float) numFrames);
			if (doCameraPan) {
				cameraTransform.position = Vector3.Lerp(originalPosition, finalPosition, (float)i / (float)numFrames);
			}
			yield return null;
		}
		SceneManager.LoadScene(scene);
	}

	IEnumerator FadeOut() {
		Vector3 originalPosition = cameraTransform.position;
		Vector3 finalPosition = new Vector3(originalPosition.x - cameraOffset, originalPosition.y, originalPosition.z);
		for (int i = numFrames - 1; i >= 0; --i) {
			panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, (float) i / (float) numFrames);
			if (doCameraPan) {
				cameraTransform.position = Vector3.Lerp(originalPosition, finalPosition, (float)i / (float)numFrames);
			}
			yield return null;
		}
		Controller.Unlock();
	}

	public void LoadScene(string scene) {
		StartCoroutine(FadeIn(scene));
	}
}
