using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using InControl;

public class Controls : MonoBehaviour {

	public GameObject controllerPrefab;
	Controller[] controllers;

	void Start() {
		controllers = new Controller[InputManager.Devices.Count];
		for (int i = 0; i < InputManager.Devices.Count; ++i) {
			controllers[i] = Instantiate(controllerPrefab).GetComponent<Controller>();
			controllers[i].inputDeviceNum = i;
		}
		Controller.canMove = true;
	}

	void Update () {
		//if (Input.GetKeyDown(KeyCode.Return)) {
		//	SceneManager.LoadScene("LevelSelect");
		//}
		for (int i = 0; i < controllers.Length; ++i) {
			GetInput(i);
		}
	}

	void GetInput(int num) {
		if (controllers[num].GetConfirmDown()) {
			AudioManager.instance.PlayClip(AudioManager.instance.confirm);
			Controller.canMove = false;
			SceneTransitioner.instance.LoadBack("LevelSelect");
		}
	}
}
