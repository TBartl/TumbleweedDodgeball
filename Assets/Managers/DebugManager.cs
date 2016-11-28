using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DebugManager : MonoBehaviour {

	public static bool vibrationsEnabled = true;

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Alpha1))
			SceneManager.LoadScene(0);	

		if (Time.timeSinceLevelLoad > 183)
			SceneManager.LoadScene(0);

		if (Input.GetKeyDown(KeyCode.Alpha2))
			vibrationsEnabled = !vibrationsEnabled;
	}
}
