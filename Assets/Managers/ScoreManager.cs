using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ScoreManager : MonoBehaviour {

	public static ScoreManager inst;
	public PlayerScoreUI addHitUI;

	int[] scores = new int[4];

	void Awake() {
		if (inst == null)
			inst = this;
	}

	public void IncrementScore(int playerID) {
		if (playerID >= 0 && playerID <= 4) {
			scores[playerID] += 1;
			addHitUI.UpdateScore(playerID, scores[playerID]);
		}
	}

	public void DecrementScore(int playerID) {
		if (playerID >= 0 && playerID <= 4) {
			scores[playerID] -= 1;
			addHitUI.UpdateScore(playerID, scores[playerID]);
		}
	}
}
