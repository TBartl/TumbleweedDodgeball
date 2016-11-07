using UnityEngine;
using System.Collections;

public class PlayerDisplay : MonoBehaviour {

	public GameObject[] playerList;

	void Start () {
		for (int i = 0; i < PlayerManager.inst.players.Count; i++) {
			playerList[i].SetActive(true);
		}
	}
}
