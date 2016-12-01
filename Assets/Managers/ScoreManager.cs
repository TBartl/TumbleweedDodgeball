using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class ScoreManager : MonoBehaviour {

	public static ScoreManager inst;
	public PlayerScoreUI addHitUI;
    public GameObject positiveScore;
    public GameObject negativeScore;
    public GameObject endScoreText;

	int[] scores = new int[4];

	void Awake() {
		if (inst == null)
			inst = this;
        addHitUI = GameObject.Find("Canvas").GetComponent<PlayerScoreUI>();
	}
    
	public void IncrementScore(int playerID) {
		if (playerID >= 0 && playerID <= 4) {
			scores[playerID] += 2;
			addHitUI.UpdateScore(playerID, scores[playerID]);
            InitNumberShown(positiveScore, playerID);
		}
	}

	public void DecrementScore(int playerID) {
		if (playerID >= 0 && playerID <= 4) {
			scores[playerID] -= 1;
			addHitUI.UpdateScore(playerID, scores[playerID]);
            InitNumberShown(negativeScore, playerID);
		}
	}

    public void SendScoresToGlobal() {
        List<int> scores_ = new List<int>();
        for (int i = 0; i < 4; ++i) scores_.Add(scores[i]);
        GlobalPlayerManager.inst.SetScores(scores_);
    }

    void InitNumberShown(GameObject prefab, int playerID) {
        Vector3 playerPos = PlayerManager.inst.GetPosition(playerID);
        Vector3 adjustedPos = new Vector3(playerPos.x, playerPos.y + 1f, playerPos.z);
        GameObject go = (GameObject)Instantiate(prefab, adjustedPos, Quaternion.Euler(0, 0, 0)); //create object
        go.transform.parent = PlayerManager.inst.GetPlayerTransform(playerID); //follow above player's head
        go.GetComponent<ScoreNumBehavior>().HandleScoreChange(); //Coroutine so that the number disappears after some time
    }
    
}
