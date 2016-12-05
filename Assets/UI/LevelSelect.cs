using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using InControl;

public class LevelSelect : MonoBehaviour {

	private int currentLevel = 0;
	public GameObject[] levelMarkers = new GameObject[4];
	public GameObject controlMarker;

	private bool controlSelected = false;

	public GameObject controllerPrefab;
	Controller[] controllers;

	void Start () {
		levelMarkers[currentLevel].SetActive(true);
		controllers = new Controller[InputManager.Devices.Count];
		for (int i = 0; i < InputManager.Devices.Count; ++i) {
			controllers[i] = Instantiate(controllerPrefab).GetComponent<Controller>();
			controllers[i].inputDeviceNum = i;
		}
		Controller.canMove = true;
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < controllers.Length; ++i) {
			GetInput(i);
		}
		//if (Input.GetKeyDown(KeyCode.LeftArrow)) {
		//	levelMarkers[currentLevel].SetActive(false);
		//	currentLevel = currentLevel == 0 ? levelMarkers.Length - 1 : currentLevel - 1;
		//	levelMarkers[currentLevel].SetActive(true);
		//} else if (Input.GetKeyDown(KeyCode.RightArrow)) {
		//	levelMarkers[currentLevel].SetActive(false);
		//	currentLevel = currentLevel == (levelMarkers.Length-1) ? 0 : currentLevel + 1;
		//	levelMarkers[currentLevel].SetActive(true);
		//} else if (Input.GetKeyDown(KeyCode.DownArrow) && !controlSelected) {
		//	controlSelected = true;
		//	levelMarkers[currentLevel].SetActive(false);
		//	controlMarker.SetActive(true);
		//} else if (Input.GetKeyDown(KeyCode.UpArrow) && controlSelected) {
		//	controlSelected = false;
		//	levelMarkers[currentLevel].SetActive(true);
		//	controlMarker.SetActive(false);
		//}

		//if (Input.GetKeyDown(KeyCode.Return)) {
		//	if (controlSelected) {
		//		SceneManager.LoadScene("Controls");
		//	} else {
		//		if (currentLevel == 0) {
		//			SceneManager.LoadScene("Tutorial");
		//		} else if (currentLevel == 1) {
		//			SceneManager.LoadScene("Levels/Cliffside");
		//		}
		//	}
		//}
	}

	void GetInput(int num) {
		if ((controllers[num].GetMainDirection().x < 0f || Input.GetKeyDown(KeyCode.A)) && !controlSelected) {
			levelMarkers[currentLevel].SetActive(false);
			currentLevel = currentLevel == 0 ? levelMarkers.Length - 1 : currentLevel - 1;
			levelMarkers[currentLevel].SetActive(true);
		}
		else if ((controllers[num].GetMainDirection().x > 0f || Input.GetKeyDown(KeyCode.D)) && !controlSelected) {
			levelMarkers[currentLevel].SetActive(false);
			currentLevel = currentLevel == (levelMarkers.Length - 1) ? 0 : currentLevel + 1;
			levelMarkers[currentLevel].SetActive(true);
		}
		else if ((controllers[num].GetMainDirection().y < 0f || Input.GetKeyDown(KeyCode.S))) {
			if (controlSelected) {
				controlSelected = false;
				currentLevel = (currentLevel < 2) ? currentLevel : currentLevel - 2;
				levelMarkers[currentLevel].SetActive(true);
				controlMarker.SetActive(false);
			} else if (currentLevel > 1) {
				controlSelected = true;
				levelMarkers[currentLevel].SetActive(false);
				controlMarker.SetActive(true);
			} else {
				levelMarkers[currentLevel].SetActive(false);
				currentLevel += 2;
				levelMarkers[currentLevel].SetActive(true);
			}
		}
		else if ((controllers[num].GetMainDirection().y > 0f || Input.GetKeyDown(KeyCode.W))) {
			if (controlSelected) {
				controlSelected = false;
				currentLevel = (currentLevel < 2) ? currentLevel + 2 : currentLevel;
				levelMarkers[currentLevel].SetActive(true);
				controlMarker.SetActive(false);
			} else if (currentLevel < 2) {
				controlSelected = true;
				levelMarkers[currentLevel].SetActive(false);
				controlMarker.SetActive(true);
			} else {
				levelMarkers[currentLevel].SetActive(false);
				currentLevel -= 2;
				levelMarkers[currentLevel].SetActive(true);
			}
		}

		if (controllers[num].GetConfirmDown() || Input.GetMouseButtonDown(2)) {
			if (controlSelected) {
				SceneManager.LoadScene("Controls");
			}
			else {
				if (currentLevel == 0) {
					Controller.canMove = false;
					SceneManager.LoadScene("Levels/Tutorial");
				}
				else if (currentLevel == 1) {
					Controller.canMove = false;
					SceneManager.LoadScene("Levels/Cliffside");
				}
				else if (currentLevel == 2) {
					Controller.canMove = false;
					SceneManager.LoadScene("Levels/Town");
				}
				else if (currentLevel == 3) {
					Controller.canMove = false;
					SceneManager.LoadScene("Levels/Train Station");
				}
			}
		}
	}
}
