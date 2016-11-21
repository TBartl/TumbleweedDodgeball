using UnityEngine;
using System.Collections;

public class TutorialManager : MonoBehaviour {

	private int curTask = 0;

	public GameObject[] Checks;

	private float timer = 0;

	public GameObject[] Players;
	public GameObject[] Directions;
	static public bool[] rHands;
	static public bool[] lHands;
	private Vector3[] startPos = new Vector3[4];
	private Quaternion[] startRot = new Quaternion[4];

	void Start () {
		rHands = new bool[Players.Length];
		lHands = new bool[Players.Length];
		for (int i = 0; i < Players.Length; ++i) {
			startPos[i] = Players[i].transform.position;
			startRot[i] = Directions[i].transform.rotation;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (timer > 0) {
			timer -= Time.deltaTime;
			return;
		}
		if (curTask == 0) {
			bool nextTask = true;
			for (int i = 0; i < Players.Length; ++i) {
				if (startPos[i] == Players[i].transform.position) {
					nextTask = false;
				} else {
					Checks[i].SetActive(true);
				}
			}
			if (nextTask) {
				TutorialMessage.nextMessage = true;
				for (int i = 0; i < Directions.Length; ++i) {
					startRot[i] = Directions[i].transform.rotation;
					Checks[i].SetActive(false);
				}
				curTask++;
			}

		} else if (curTask == 1) {
			bool nextTask = true;
			for (int i = 0; i < Directions.Length; ++i) {
				if (startRot[i] == Directions[i].transform.rotation) {
					nextTask = false;
				} else {
					Checks[i].SetActive(true);
				}
			}
			if (nextTask) {
				TutorialMessage.nextMessage = true;
				curTask++;
				for (int i = 0; i < Players.Length; ++i) {
					rHands[i] = false;
					Checks[i].SetActive(false);
				}
			}
		} else if (curTask == 2) {
			bool nextTask = true;
			for (int i = 0; i < rHands.Length; ++i) {
				if(!rHands[i]) nextTask = false; 
				else {
					Checks[i].SetActive(true);
				}
			}
			if (nextTask) {
				TutorialMessage.nextMessage = true;
				curTask++;
				for (int i = 0; i < Players.Length; ++i) {
					lHands[i] = false;
					Checks[i].SetActive(false);
				}
			}
		} else if (curTask == 3) {
			bool nextTask = true;
			for (int i = 0; i < lHands.Length; ++i) {
				if(!lHands[i]) nextTask = false;
				else {
					Checks[i].SetActive(true);
				}
			}
			if (nextTask) {
				TutorialMessage.nextMessage = true;
				curTask++;
//				for (int i = 0; i < Players.Length; ++i) {
//					lHands[i] = false;
//				}
			}
		}
	}
}
