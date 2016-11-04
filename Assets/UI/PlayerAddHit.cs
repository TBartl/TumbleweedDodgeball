using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerAddHit : MonoBehaviour {

	public GameObject[] playerHitCount;
	private int[] counts = {0,0,0,0};

	void Start () {
		for (int i = 0; i < playerHitCount.Length; ++i) {
			Text temp = playerHitCount[i].GetComponent<Text>();
			temp.text = "Hits:"+counts[i];
		}
	}

	public void AddHit (int playerID) {
		counts[playerID]++;
		Text temp = playerHitCount[playerID].GetComponent<Text>();
		temp.text = "Hits:"+counts[playerID];
	}

}
