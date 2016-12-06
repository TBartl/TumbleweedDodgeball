using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour {

    private float curTime = 120;
    private Text timer;

    private Color originalColor;
    private Color flashingColor;
    private bool isFlashing = false;
    private bool isShowingScore = false;
    private float interval = 0.1f;
	static public bool play;

    private bool restartGame;

	void Start () {
		timer = GetComponent<Text>();
		play = false;
        originalColor = timer.color;
        flashingColor = Color.red;
	}

	void FixedUpdate () {
		if (curTime > 0) {
			float min = Mathf.Floor(curTime/60);
			float sec = Mathf.Floor(curTime%60);
			if (play) curTime -= Time.deltaTime;
            if (sec < 10) {
                timer.text = min + ":0" + sec;
                if(!isFlashing && curTime < 10) {
                    StartCoroutine(FlashTimer());
                    isFlashing = true;
                }
            }
            else timer.text = min + ":" + sec;
		} else {
            //Display Score and reload screen on pressedbutton
            ScoreManager.inst.SendScoresToGlobal();
            SceneManager.LoadScene("EndScene");
        }
	}

    IEnumerator FlashTimer() {
        while(curTime > 0) {
            if (Mathf.RoundToInt(Time.time / interval) % 2 == 0) timer.color = originalColor;
            else timer.color = flashingColor;
            yield return null;
        }
        timer.color = originalColor;
    }
}
