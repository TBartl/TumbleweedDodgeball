using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour {

	private float curTime = 180;
	private Text timer;

	static public bool play;

	void Start () {
		timer = GetComponent<Text>();
		play = false;
	}

	void FixedUpdate () {
		if (curTime > 0) {
			float min = Mathf.Floor(curTime/60);
			float sec = Mathf.Floor(curTime%60);
			if (play) curTime -= Time.deltaTime;
			if (sec < 10)timer.text = min+":0"+sec;
			else timer.text = min+":"+sec;
		} else {
			Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
		}
	}
}
