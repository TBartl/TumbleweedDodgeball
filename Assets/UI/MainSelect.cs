using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainSelect : MonoBehaviour {

	private int curSelect = 0;
	public GameObject[] Selectors;
	
	void Start () {
		for (int i = 0; i < Selectors.Length; ++i) {
			if (i != curSelect) Selectors[i].SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.UpArrow)) {
			Selectors[curSelect].SetActive(false);
			curSelect--;
			if (curSelect < 0) curSelect = Selectors.Length - 1;
			Selectors[curSelect].SetActive(true);
		} else if (Input.GetKeyDown(KeyCode.DownArrow)) {
			Selectors[curSelect].SetActive(false);
			curSelect++;
			if (curSelect > Selectors.Length - 1) curSelect = 0;
			Selectors[curSelect].SetActive(true);
		} else if (Input.GetKeyDown(KeyCode.Return)) {
			if (curSelect == 3) {}//go to controls screen
			else NumPlayers.inst.SetNumPlayers(curSelect+2);
			SceneManager.LoadScene("CharSelect");
		}
	}
}
