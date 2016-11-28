using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

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
	static public bool[] dashed;

	void Start () {
		rHands = new bool[Players.Length];
		lHands = new bool[Players.Length];
		dashed = new bool[Players.Length];
		for (int i = 0; i < Players.Length; ++i) {
			startPos[i] = Players[i].transform.position;
			startRot[i] = Directions[i].transform.rotation;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (timer > 0) {
			timer -= Time.deltaTime;
			if (timer <= 0) {
				TutorialMessage.nextMessage = true;
				for (int i = 0; i < Directions.Length; ++i) {
					startRot[i] = Directions[i].transform.rotation;
					rHands[i] = false;
					lHands[i] = false;
					Checks[i].SetActive(false);
				}
			}
 			return;
		}
		if (curTask == 0) {
			bool nextTask = true;
			for (int i = 0; i < Players.Length; ++i) {
				if (startPos[i] == Players[i].transform.position && GlobalPlayerManager.inst.IsInGame(i)) {
					nextTask = false;
				} else if (GlobalPlayerManager.inst.IsInGame(i)) {
					Checks[i].SetActive(true);
				}
			}
			if (nextTask) {
				timer = 2;
				curTask++;
			}

		} else if (curTask == 1) {
			bool nextTask = true;
			for (int i = 0; i < Directions.Length; ++i) {
				if (startRot[i] == Directions[i].transform.rotation && GlobalPlayerManager.inst.IsInGame(i)) {
					nextTask = false;
				} else {
					Checks[i].SetActive(true);
				}
			}
			if (nextTask) {
				timer = 2;
				curTask++;
			}
		} else if (curTask == 2) {
			bool nextTask = true;
			for (int i = 0; i < rHands.Length; ++i) {
				if(!rHands[i] && GlobalPlayerManager.inst.IsInGame(i)) nextTask = false; 
				else {
					Checks[i].SetActive(true);
				}
			}
			if (nextTask) {
				timer = 2;
				curTask++;
			}
		} else if (curTask == 3) {//3
			bool nextTask = true;
			for (int i = 0; i < lHands.Length; ++i) {
				if(!lHands[i] && GlobalPlayerManager.inst.IsInGame(i)) nextTask = false;
				else if (GlobalPlayerManager.inst.IsInGame(i)){
					Checks[i].SetActive(true);
				}
			}
			if (nextTask) {
				timer = 2;
				curTask++;
			}
		} else if (curTask == 4) {
			bool nextTask = true;
			for (int i = 0; i < Players.Length; ++i) {
				if (Players[i].GetComponent<Rigidbody2D>().velocity.magnitude > 5 && GlobalPlayerManager.inst.IsInGame(i)) {
					nextTask = false;
				} else if (GlobalPlayerManager.inst.IsInGame(i)) {
					Checks[i].SetActive(true);
				}
			}
			for (int i = 0; i < dashed.Length; ++i) {
				if(!dashed[i]  && GlobalPlayerManager.inst.IsInGame(i)) nextTask = false;
				else if (GlobalPlayerManager.inst.IsInGame(i)) {
					Checks[i].SetActive(true);
				}
			}
			if (nextTask) {
				SceneManager.LoadScene("MainMenu");
			}
		}
	}
}
