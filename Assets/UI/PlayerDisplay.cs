using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerDisplay : MonoBehaviour {

	public GameObject[] playerList;

	void Start () {
		for (int i = 0; i < PlayerManager.inst.players.Count; i++) {
			playerList[i].SetActive(true);
			foreach (Text t in playerList[i].GetComponentsInChildren<Text>()) {
				t.color = PlayerManager.inst.GetColor(i);
			}
		}
	}
}
