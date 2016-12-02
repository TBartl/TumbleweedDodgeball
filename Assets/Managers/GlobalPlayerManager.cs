using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class GlobalPlayerManager : MonoBehaviour {

	public static GlobalPlayerManager inst;

	public int players;

	public List<PlayerColor> materials;
    public List<int> scores = new List<int>();
    private bool[] inGame = new bool[4];

	void Awake() {
		if (inst == null)
			inst = this;
		for (int i = 0; i < inGame.Length; ++i) {
			inGame[i] = false;
		}
	}
	
	public void SetMaterial(int playerID, PlayerColor Mat) {
		materials[playerID] = Mat;
	}

	public void SetNumPlayers (int numP) {
		players = numP;
	} 

	public int GetNumPlayers () {
		return players;
	}

	public void SetInGameTrue (int PlayerNum) {
		inGame[PlayerNum] = true;
	}

	public void SetInGameFalse (int PlayerNum) {
		inGame[PlayerNum] = false;
	}

	public bool IsInGame (int PlayerNum) {
		return inGame[PlayerNum];
	}

    public void SetScores(List<int> scores_) {
        scores = scores_;
    }
}
