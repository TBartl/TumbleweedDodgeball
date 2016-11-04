using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartTimer : MonoBehaviour {

	private float curTime = 3.9f;
	private Text timer;

	private bool go = false;
	private float goTime = 1;

	void Start () {
		timer = GetComponent<Text>();
	}

	void FixedUpdate () {
		if (!go) {
			if ( Mathf.Floor(curTime) > 0) {
				float sec = Mathf.Floor(curTime);
				timer.text = sec+"";
				curTime -= Time.deltaTime;
			} else {
				timer.text = "GO";
				go = true;
			}
		} else {
			goTime -= Time.deltaTime;
			if (goTime <= 0) {
				timer.text = "";
				GameTimer.play = true;
			}
		}
	}
}
