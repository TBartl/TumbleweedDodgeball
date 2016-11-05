using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerAddHit : MonoBehaviour {

	public GameObject[] playerHitCount = new GameObject[4];
	static private Text[] hitText  = new Text[4];
	private static int[] counts = {0,0,0,0};

	void Start () {
		for (int i = 0; i < playerHitCount.Length; ++i) {
			hitText[i] = playerHitCount[i].GetComponent<Text>();
			hitText[i].text = "Hits:"+counts[i];
		}
	}

	public static void AddHit (int playerID) {
		counts[playerID]++;
		hitText[playerID].text = "Hits:"+counts[playerID];
	}

}
