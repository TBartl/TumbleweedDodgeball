using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using InControl;

public class TitleScreenManager : MonoBehaviour {

	private Controller[] controllers;
    private bool checkForInput = true;

	public GameObject controllerPrefab;

	public GameObject textGood;
	public GameObject textBad;

	// Use this for initialization
	void Start () {
		if (InputManager.Devices.Count <= 1) {
			textGood.SetActive(false);
			textBad.SetActive(true);
			checkForInput = false;
			return;
		}
		controllers = new Controller[4];
		for (int i = 0; i < 4; ++i) {
			controllers[i] = Instantiate(controllerPrefab).GetComponent<Controller>();
			controllers[i].inputDeviceNum = i;
		}
	}
	
	// Update is called once per frame
	void Update () {
        if (!checkForInput) return;
		for (int i = 0; i < 4; ++i) {
			GetInput(i);
		}
	}

	void GetInput(int i) {
		if (controllers[i] == null) {
			return;
		}
		Controller controller = controllers[i];

		if (controller.GetConfirmDown()) {
			AudioManager.instance.PlayClip(AudioManager.instance.confirm);
			SceneTransitioner.instance.LoadScene("MainMenu");
		}
	}
}
