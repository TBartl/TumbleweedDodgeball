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

	bool loading;

	static bool back = false;

	void Awake() {
		Controller.Lock();
		instance = this;
		loading = true;
		cameraTransform = Camera.main.transform;
		StartCoroutine(FadeOut());
		if (doCameraPan) {
			if (back) {
				StartCoroutine(PanInLeft());
			}
			else {
				StartCoroutine(PanInRight());
			}
		}
	}

	IEnumerator FadeIn(string scene) {
		Controller.Lock();
		for (int i = 0; i < numFrames; ++i) {
			panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, (float) i / (float) numFrames);
			yield return null;
		}
		SceneManager.LoadScene(scene);
	}

	IEnumerator FadeOut() {
		for (int i = numFrames - 1; i >= 0; --i) {
			panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, (float) i / (float) numFrames);
			yield return null;
		}
		Controller.Unlock();
		loading = false;
	}

	IEnumerator PanInRight() {
		Quaternion originalRotation = cameraTransform.rotation;
		Quaternion finalRotation = originalRotation * Quaternion.AngleAxis(-cameraTransitionAngle, new Vector3(0, 1, 0));
		for (int i = numFrames - 1; i >= 0; --i) {
			cameraTransform.rotation = Quaternion.Lerp(originalRotation, finalRotation, (float)i / (float)numFrames);
			yield return null;
		}
	}

	IEnumerator PanInLeft() {
		Quaternion originalRotation = cameraTransform.rotation;
		Quaternion finalRotation = originalRotation * Quaternion.AngleAxis(cameraTransitionAngle, new Vector3(0, 1, 0));
		for (int i = numFrames - 1; i >= 0; --i) {
			cameraTransform.rotation = Quaternion.Lerp(originalRotation, finalRotation, (float)i / (float)numFrames);
			yield return null;
		}
	}

	IEnumerator PanOutRight() {
		Quaternion originalRotation = cameraTransform.rotation;
		Quaternion finalRotation = originalRotation * Quaternion.AngleAxis(cameraTransitionAngle, new Vector3(0, 1, 0));
		for (int i = 0; i < numFrames; ++i) {
			cameraTransform.rotation = Quaternion.Lerp(originalRotation, finalRotation, (float)i / (float)numFrames);
			yield return null;
		}
	}

	IEnumerator PanOutLeft() {
		Quaternion originalRotation = cameraTransform.rotation;
		Quaternion finalRotation = originalRotation * Quaternion.AngleAxis(-cameraTransitionAngle, new Vector3(0, 1, 0));
		for (int i = 0; i < numFrames; ++i) {
			cameraTransform.rotation = Quaternion.Lerp(originalRotation, finalRotation, (float)i / (float)numFrames);
			yield return null;
		}
	}

	public void LoadNext(string scene) {
		if (!loading) {
			loading = true;
			back = false;
			StartCoroutine(FadeIn(scene));
			if (doCameraPan) {
				StartCoroutine(PanOutRight());
			}
		}
	}

	public void LoadBack(string scene) {
		if (!loading) {
			loading = true;
			back = true;
			StartCoroutine(FadeIn(scene));
			if (doCameraPan) {
				StartCoroutine(PanOutLeft());
			}
		}
	}
}
