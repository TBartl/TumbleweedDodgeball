using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour {
	public static PlayerManager inst;

	[HideInInspector] 
	public List<GameObject> players;
	public List<Color> colors;
	public List<Material> materials;


	void Awake() {
		if (inst == null)
			inst = this;
		players = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
		for (int i = 0; i < players.Count; i++) {
			players[i].GetComponent<PlayerData>().num = i;
			players[i].GetComponent<Controller>().inputDeviceNum = i;
		}
			
		for (int i = 0; i < players.Count; i++) {
			players[i].GetComponent<PlayerData>().num = i;
			players[i].GetComponent<Controller>().inputDeviceNum = i;
		}
	}

	void Start() {
		materials = GlobalPlayerManager.inst.materials;
		int curCount = 0;
		for (int i = 0; i < 4; i++) {
			if (!GlobalPlayerManager.inst.IsInGame(i)){
				players[i].SetActive(false);
			} else {
				players[i].GetComponent<PlayerData>().num = curCount;
				curCount++;
			}
		}
	}

    public bool playerIsActive(int playerID) {
        return playerID < players.Count;
    }

    public Transform GetPlayerTransform(int playerID) {
        return players[playerID].transform;
    }

    public Vector3 GetPosition(int playerID) {
        return players[playerID].transform.position;
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

    public void UnfreezePlayers() {
        Controller.canMove = true;
    }
    
    public bool GetRestartFromPlayers() {
        foreach(GameObject go in players) {
            if (go.GetComponent<Controller>().GetRestartPressed()) return true;
        }
        return false;
    }
}
