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
		float prev = 4;
		for (float t = 3; t > 0; t -= Time.deltaTime) {
			timer.text = Mathf.Ceil(t).ToString();
			if (Mathf.Ceil(t) != prev) {
				AudioManager.instance.PlayClip(AudioManager.instance.timer);
				prev = Mathf.Ceil(t);
			}
			yield return null;
		}

        PlayerManager.inst.UnfreezePlayers();
        timer.text = "GO";

		AudioManager.instance.PlayClip(AudioManager.instance.timer);
		AudioManager.instance.StartMusic();
		for (float t = 0; t < 1f; t += Time.deltaTime)
			yield return null;

		timer.text = "";
		GameTimer.play = true;
	}


}
