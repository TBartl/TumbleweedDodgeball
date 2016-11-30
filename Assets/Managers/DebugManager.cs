using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class DebugManager : MonoBehaviour {
	public static DebugManager inst;

	public static bool vibrationsEnabled = true;
	public static bool noChargeDecrease = true;
	public static bool useRandomPrefabs = false;
	


	public List<GameObject> randomPrefabs;

	void Awake() {
		if (!inst)
			inst = this;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Alpha1))
			SceneManager.LoadScene(0);	

		if (Time.timeSinceLevelLoad > 183)
			SceneManager.LoadScene(0);

		if (Input.GetKeyDown(KeyCode.Alpha2))
			vibrationsEnabled = !vibrationsEnabled;

		if (Input.GetKeyDown(KeyCode.Alpha3))
			noChargeDecrease = !noChargeDecrease;

		if (Input.GetKeyDown(KeyCode.Alpha4))
			useRandomPrefabs = !useRandomPrefabs;

		if (Input.GetKeyDown(KeyCode.Alpha5)) {
			if (Time.timeScale == 1f)
				Time.timeScale = 10f;
			else if (Time.timeScale == 10f)
				Time.timeScale = .1f;
			else if (Time.timeScale == .1f)
				Time.timeScale = 1f;

		}
	}
}
