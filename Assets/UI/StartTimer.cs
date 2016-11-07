using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartTimer : MonoBehaviour {

	private Text timer;

	void Start () {
		timer = GetComponent<Text>();
		StartCoroutine(CountDown());
	}

	IEnumerator CountDown () {
		for (float t = 3; t > 0; t -= Time.deltaTime) {
			timer.text = Mathf.Ceil(t).ToString();
			yield return null;
		}

		timer.text = "GO";
		for (float t = 0; t < 1f; t += Time.deltaTime)
			yield return null;

		timer.text = "";
		GameTimer.play = true;
	}


}
