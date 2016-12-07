using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerScoreUI : MonoBehaviour {
	
	public Text[] hitText = new Text[4];


	void Start () {
		for (int i = 0; i < 4; ++i) {
			hitText[i].text = "Score: 0";
		}
	}

	public void UpdateScore (int playerID, int amount) {
		hitText[playerID].text = "Score: " + amount;
	}
}
