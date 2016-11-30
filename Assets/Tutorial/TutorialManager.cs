using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour {

	public static TutorialManager inst;

	public float waitTime = 2f;
	public Text text;
	public GameObject[] Checks;

	public GameObject[] Players;
	public GameObject[] Directions;
	public bool[] rHands = new bool[4];
	public bool[] lHands = new bool[4];
	private Vector3[] startPos = new Vector3[4];
	private Quaternion[] startRot = new Quaternion[4];
	public bool[] dashed = new bool[4];

	void Awake() {
		inst = this;
	}

	void Start () {
		for (int i = 0; i < Players.Length; ++i) {
			startPos[i] = Players[i].transform.position;
			startRot[i] = Directions[i].transform.rotation;
		}

		StartCoroutine(RunTutorial());
	}

	IEnumerator RunTutorial() {
		ChangeTask();
		yield return new WaitForSeconds(3);

		text.text = "Use Left Stick to walk";
		while (!CheckTaskDone()) {
			for (int i = 0; i < 4; i++) {
				if (Players[i].activeSelf == false)
					continue;
				if (startPos[i] != Players[i].transform.position)
					Checks[i].SetActive(true);
			}
			yield return null;
		}		
		yield return new WaitForSeconds(waitTime);
		ChangeTask();

		text.text = "Use Right Stick to Aim";
		while (!CheckTaskDone()) {
			for (int i = 0; i < 4; i++) {
				if (Players[i].activeSelf == false)
					continue;
				if (startRot[i] != Directions[i].transform.rotation)
					Checks[i].SetActive(true);
			}
			yield return null;
		}
		yield return new WaitForSeconds(waitTime);
		ChangeTask();

		text.text = "Use Right Bumper to pick up and throw balls with Right Hand";
		while (!CheckTaskDone()) {
			for (int i = 0; i < 4; i++) {
				if (Players[i].activeSelf == false)
					continue;
				if (Players[i].GetComponentInChildren <PlayerHands>().balls[1])
					Checks[i].SetActive(true);
			}
			yield return null;
		}
		yield return new WaitForSeconds(waitTime);
		ChangeTask();

		text.text = "Use Left Bumper to pick up and throw balls with Left Hand";
		while (!CheckTaskDone()) {
			for (int i = 0; i < 4; i++) {
				if (Players[i].activeSelf == false)
					continue;
				if (Players[i].GetComponentInChildren<PlayerHands>().balls[0])
					Checks[i].SetActive(true);
			}
			yield return null;
		}
		yield return new WaitForSeconds(waitTime);
		ChangeTask();

		text.text = "Use A to Dash in the Direction of Movement";
		while (!CheckTaskDone()) {
			for (int i = 0; i < 4; i++) {
				if (Players[i].activeSelf == false)
					continue;
				if (Players[i].GetComponent<Rigidbody2D>().velocity.magnitude > 8)
					Checks[i].SetActive(true);
			}
			yield return null;
		}
		yield return new WaitForSeconds(waitTime);
		ChangeTask();

		SceneManager.LoadScene("MainMenu");

	}

	void ChangeTask() {
		for (int i = 0; i < 4; ++i) {
			Checks[i].SetActive(false);
		}
	}

	bool CheckTaskDone() {
		for (int i = 0; i < 4; ++i) {
			if (Players[i].activeSelf == false)
				continue;
			if (Checks[i].activeSelf == false ) {
				return false;
			}
		}
		return true;
	}
}
