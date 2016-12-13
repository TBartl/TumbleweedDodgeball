using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransitioner : MonoBehaviour {

	public static SceneTransitioner instance;

	public Image panel;
	public int numFrames;
	public float cameraTransitionAngle;

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
		Quaternion originalRotation = cameraTransform.rotation;
		Quaternion finalRotation = originalRotation * Quaternion.AngleAxis(cameraTransitionAngle, new Vector3(0, 1, 0));
		for (int i = 0; i < numFrames; ++i) {
			panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, (float) i / (float) numFrames);
			if (doCameraPan) {
				cameraTransform.rotation = Quaternion.Lerp(originalRotation, finalRotation, (float)i / (float)numFrames);
			}
			yield return null;
		}
		SceneManager.LoadScene(scene);
	}

	IEnumerator FadeOut() {
		Quaternion originalRotation = cameraTransform.rotation;
		Quaternion finalRotation = originalRotation * Quaternion.AngleAxis(-cameraTransitionAngle, new Vector3(0, 1, 0));
		for (int i = numFrames - 1; i >= 0; --i) {
			panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, (float) i / (float) numFrames);
			if (doCameraPan) {
				cameraTransform.rotation = Quaternion.Lerp(originalRotation, finalRotation, (float) i / (float) numFrames);
			}
			yield return null;
		}
		Controller.Unlock();
	}

	public void LoadScene(string scene) {
		StartCoroutine(FadeIn(scene));
	}
}
