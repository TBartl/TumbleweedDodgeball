using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class GlobalPlayerManager : MonoBehaviour {

	public static GlobalPlayerManager inst;

	public int players;
	public List<PlayerColor> materials;

	void Awake() {
		if (inst == null)
			inst = this;
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
}
