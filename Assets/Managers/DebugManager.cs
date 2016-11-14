using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DebugManager : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Alpha1))
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);	

		if (Time.timeSinceLevelLoad > 183)
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
