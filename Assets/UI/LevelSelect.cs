using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using InControl;

public class LevelSelect : MonoBehaviour {

	private int currentLevel = 0;
	public GameObject[] levelMarkers = new GameObject[2];
	public GameObject controlMarker;

	private bool controlSelected = false;


	void Start () {
		levelMarkers[currentLevel].SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.LeftArrow)) {
			levelMarkers[currentLevel].SetActive(false);
			currentLevel = currentLevel == 0 ? levelMarkers.Length - 1 : currentLevel - 1;
			levelMarkers[currentLevel].SetActive(true);
		} else if (Input.GetKeyDown(KeyCode.RightArrow)) {
			levelMarkers[currentLevel].SetActive(false);
			currentLevel = currentLevel == (levelMarkers.Length-1) ? 0 : currentLevel + 1;
			levelMarkers[currentLevel].SetActive(true);
		} else if (Input.GetKeyDown(KeyCode.DownArrow) && !controlSelected) {
			controlSelected = true;
			levelMarkers[currentLevel].SetActive(false);
			controlMarker.SetActive(true);
		} else if (Input.GetKeyDown(KeyCode.UpArrow) && controlSelected) {
			controlSelected = false;
			levelMarkers[currentLevel].SetActive(true);
			controlMarker.SetActive(false);
		}

		if (Input.GetKeyDown(KeyCode.Return)) {
			if (controlSelected) {
				SceneManager.LoadScene("Controls");
			} else {
				if (currentLevel == 0) {
					SceneManager.LoadScene("Tutorial");
				} else if (currentLevel == 1) {
					SceneManager.LoadScene("Levels/Cliffside");
				}
			}
		}
	}
}
