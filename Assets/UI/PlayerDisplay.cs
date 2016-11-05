using UnityEngine;
using System.Collections;

public class PlayerDisplay : MonoBehaviour {

	public GameObject[] playerList;

	void Start () {
		for (int i = playerList.Length - 1; i >= 0; --i) {
			if (i < GameManager.instance.players.Length) break;
			playerList[i].SetActive(false);
		}
	}
}
