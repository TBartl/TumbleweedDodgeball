using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using InControl;

public class Controls : MonoBehaviour {

	void Update () {
		if (Input.GetKeyDown(KeyCode.Return)) {
			SceneManager.LoadScene("LevelSelect");
		}
	}
}
