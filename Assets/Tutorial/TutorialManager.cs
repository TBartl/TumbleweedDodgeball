using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

enum TutorialText {
	leftStickWalk,
	rightStickAim,
	aDash,
	punch,
	rightHandThrow,
	leftHandThrow
}

public class TutorialManager : MonoBehaviour {

	public static TutorialManager inst;

	public float waitTime = 2f;
	public Image textImage;
	public List<Sprite> textSprites; 
	public GameObject[] Checks;
	public GameObject[] CheckEnd;

	public GameObject[] Players;
	public GameObject[] Balls;
	public GameObject[] Directions;
	public bool[] rHandsUp = new bool[4];
	public bool[] lHandsUp = new bool[4];
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
			Balls[i].transform.position = Players[i].transform.position;
			Balls[i].transform.SetParent(Players[i].transform);
			Balls[i].SetActive(false);
		}
		for (int i = 0; i < 4; ++i) {
			if (Players[i].activeSelf) {
				Image temp = Checks[i].GetComponent<Image>();
				temp.color = PlayerManager.inst.GetColor(Players[i].GetComponent<PlayerData>().num);
				temp = CheckEnd[i].GetComponent<Image>();
				temp.color = PlayerManager.inst.GetColor(Players[i].GetComponent<PlayerData>().num);
			}
		}
 		StartCoroutine(RunTutorial());
	}

	IEnumerator RunTutorial() {
		ChangeTask();
		yield return new WaitForSeconds(3);
		
		textImage.sprite = textImage.sprite = textSprites[(int)TutorialText.leftStickWalk];
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
		for (int i = 0; i < 4; i++)
			startRot[i] = Directions[i].transform.rotation;

		textImage.sprite = textImage.sprite = textSprites[(int)TutorialText.rightStickAim];
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

		textImage.sprite = textImage.sprite = textSprites[(int)TutorialText.aDash];
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

		textImage.sprite = textImage.sprite = textSprites[(int)TutorialText.punch];
		while (!CheckTaskDone()) {
			for (int i = 0; i < 4; i++) {
				if (Players[i].activeSelf == false)
					continue;
				if (Players[i].GetComponentInChildren<PlayerHands>().GetIsPunching(0) || Players[i].GetComponentInChildren<PlayerHands>().GetIsPunching(1))
					Checks[i].SetActive(true);
			}
			yield return null;
		}
		yield return new WaitForSeconds(waitTime);
		ChangeTask();
		for (int i = 0; i < 4; ++i) {
			Balls[i].SetActive(true);
			Balls[i].transform.SetParent(null);
		}

		textImage.sprite = textImage.sprite = textSprites[(int)TutorialText.rightHandThrow];
		while (!CheckTaskDone()) {
			for (int i = 0; i < 4; i++) {
				if (Players[i].activeSelf == false)
					continue;
				if (Players[i].GetComponentInChildren<PlayerHands>().balls[1])
					rHandsUp[i] = true;
				else if (rHandsUp[i])
					Checks[i].SetActive(true);
			}
			yield return null;
		}
		yield return new WaitForSeconds(waitTime);
		ChangeTask();

		textImage.sprite = textImage.sprite = textSprites[(int)TutorialText.leftHandThrow];
		while (!CheckTaskDone()) {
			for (int i = 0; i < 4; i++) {
				if (Players[i].activeSelf == false)
					continue;
				if (Players[i].GetComponentInChildren<PlayerHands>().balls[0])
					lHandsUp[i] = true;
				else if (lHandsUp[i])
					Checks[i].SetActive(true);
			}
			yield return null;
		}
		yield return new WaitForSeconds(waitTime);
		ChangeTask();

		SceneTransitioner.instance.LoadScene("LevelSelect");
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
