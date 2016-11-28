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
			scores[playerID] += 1;
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

    void InitNumberShown(GameObject prefab, int playerID) {
        Vector3 playerPos = PlayerManager.inst.GetPosition(playerID);
        Vector3 adjustedPos = new Vector3(playerPos.x, playerPos.y + 1f, playerPos.z);
        GameObject go = (GameObject)Instantiate(prefab, adjustedPos, Quaternion.Euler(0, 0, 0)); //create object
        go.transform.parent = PlayerManager.inst.GetPlayerTransform(playerID); //follow above player's head
        go.GetComponent<ScoreNumBehavior>().HandleScoreChange(); //Coroutine so that the number disappears after some time
    }

    public void DeactivateEndScore() {
        endScoreText.SetActive(false);
    }

    public void DisplayEndScore() {
        int winner = GetWinner();
        if(winner == -1) endScoreText.GetComponent<Text>().text = "\t\tIt's a draw!\nPress A to play again";
        else endScoreText.GetComponent<Text>().text = "\tPlayer " + winner + " wins!\nPress A to play again";
        endScoreText.SetActive(true);
    }

    int GetWinner() {
        int winningID = -1;
        int minScore = System.Int32.MinValue;
        for(int i = 0; i < 4; ++i) {
            if (scores[i] > minScore && PlayerManager.inst.playerIsActive(i)) winningID = i;
        }

        //check for draw
        for (int i = 0; i < 4; ++i) {
            if (winningID == -1) break;
            if (i == winningID) continue;
            if (scores[i] == scores[winningID] && PlayerManager.inst.playerIsActive(i)) winningID = -1;
        }
        return winningID;
    }
}
