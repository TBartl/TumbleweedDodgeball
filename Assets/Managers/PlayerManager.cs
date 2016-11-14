using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour {
	public static PlayerManager inst;

//	[HideInInspector] 
	public List<GameObject> players;
	public List<Color> colors;
	public List<Material> materials;

	void Awake() {
		if (inst == null)
			inst = this;
		players = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
		materials = NumPlayers.inst.materials;
		for (int i = 0; i < 4-NumPlayers.inst.GetNumPlayers(); ++i) {
			players[i].SetActive(false);
		}
	}


	public Color GetColor(int playerID) {
		return colors[playerID];
	}

	public Material GetMaterial(int playerID) {
		return materials[playerID];
	}
	public void SetMaterials(List<Material> mats) {
		materials = mats;
	}
}
