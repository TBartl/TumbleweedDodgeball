using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialMessage : MonoBehaviour {

	private string[] messages = {"Use Left Stick to walk", "Use Right Stick to Aim", "Use Right Bumper to pick up and throw balls with Right Hand"
	, "Use Left Bumper to pick up and throw balls with Left Hand"};
	private int curMessage = 0;
	static public bool nextMessage;

	static private Text Message;

	void Start () {
		Message = GetComponent<Text>();
		nextMessage = true;
	}
	

	void Update () {
		if (nextMessage) {
			nextMessage = false;
			SetMessageText(messages[curMessage++]);
			if (curMessage >= messages.Length) {
				//load main menu
			}
		}
	}


	static public void SetMessageText (string mes) {
		Message.text = mes;
	}
}
