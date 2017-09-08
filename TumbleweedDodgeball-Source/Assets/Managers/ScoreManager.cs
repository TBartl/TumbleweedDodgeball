using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class ScoreManager : MonoBehaviour {

	public static ScoreManager inst;
	public PlayerScoreUI addHitUI;
    public GameObject positiveScore;
    public GameObject negativeScore;
	public GameObject positiveScore3;
    public GameObject endScoreText;

	int[] scores = new int[4];
	int leader = -1;

	void Awake() {
		if (inst == null)
			inst = this;
        addHitUI = GameObject.Find("Canvas").GetComponent<PlayerScoreUI>();
	}
    
	public void IncrementScore(int playerID, int hitID) {
		if (playerID >= 0 && playerID <= 4) {
			if (GetLeader() == hitID) {
				scores[playerID] += 3;
				InitNumberShown(positiveScore3, playerID);
			}
			else {
				scores[playerID] += 2;
				InitNumberShown(positiveScore, playerID);
			}
			addHitUI.UpdateScore(playerID, scores[playerID]);
			UpdateLeader();
		}
	}

	public void DecrementScore(int playerID) {
		if (playerID >= 0 && playerID <= 4) {
            if(scores[playerID] != 0) {
                scores[playerID]--;
                addHitUI.UpdateScore(playerID, scores[playerID]);
                InitNumberShown(negativeScore, playerID);
				UpdateLeader();
			}
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
    
	void UpdateLeader() {
		bool tied = false;
		leader = 0;
		for (int i = 1; i < 4; ++i) {
			if (scores[i] == scores[leader])
				tied = true;
			else if (scores[i] > scores[leader]) {
				tied = false;
				leader = i;
			}
		}
		// Don't show tied people in game otherwise everyone would start with a crown
		if (tied)
			leader = -1;
	}

	public int GetLeader() {
		return leader;
	}
}
