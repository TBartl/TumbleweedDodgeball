using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class NumPlayers : MonoBehaviour {

	public static NumPlayers inst;

	public int players;
	public List<Material> materials;

	void Awake() {
		if (inst == null)
			inst = this;
		DontDestroyOnLoad(this.gameObject);
	}
	
	public void SetMaterial(int playerID, Material Mat) {
		materials[playerID] = Mat;
	}

	public void SetNumPlayers (int numP) {
		players = numP;
	} 

	public int GetNumPlayers () {
		return players;
	}
}
