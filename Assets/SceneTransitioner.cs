using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransitioner : MonoBehaviour {

	public static SceneTransitioner instance;

	public Image panel;
	public int numFrames;

	void Awake() {
		instance = this;
		StartCoroutine(FadeOut());
	}

	IEnumerator FadeIn(string scene) {
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
	}

	public void LoadScene(string scene) {
		StartCoroutine(FadeIn(scene));
	}
}
