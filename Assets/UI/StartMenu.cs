using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using InControl;

public class StartMenu : MonoBehaviour {

	private int curSelect = 0;

	public PlayerColor[] materials;
	public static int[] currentMat = new int[4];

	public GameObject[] Join;
	public GameObject[] Joined;
	public GameObject[] JoinedUI;
	public GameObject[] playerReady;

	private static bool[] inGame = new bool[4];
	private bool[] pReady = new bool[4];

	private int numPlayers;

	private Controller[] controllers;

	public GameObject controllerPrefab;
	
	void Start () {
		controllers = new Controller[inGame.Length];
		for (int i = 0; i < inGame.Length; ++i) {
			if (inGame[i]) {
				GlobalPlayerManager.inst.SetInGameTrue(i);
				Join[i].SetActive(false);
				Joined[i].SetActive(true);
				JoinedUI[i].SetActive(true);
				numPlayers++;
				while (CompareColor(i)) {
					currentMat[i]++;
					if (currentMat[i] > 9) currentMat[i] = 0;
				}
				foreach (Renderer r in Joined[i].GetComponentsInChildren<Renderer>()) {
					r.material = materials[currentMat[i]].mat;
				}
				GlobalPlayerManager.inst.SetMaterial(i, materials[currentMat[i]]);
			}
			pReady[i] = false;
			controllers[i] = Instantiate(controllerPrefab).GetComponent<Controller>();
			controllers[i].inputDeviceNum = i;
		}
	}

	void Update () {

		GetInput(0);
		GetInput(1);
		GetInput(2);
		GetInput(3);

		bool start = true;
		int count = 0;

		for (int i = 0; i < inGame.Length; ++i) {
			if (inGame[i]) {
				++count;
			}
		}
		if (count < 2)
			start = false;
		else {
			for (int i = 0; i < inGame.Length; ++i) {
				if (inGame[i] && !pReady[i]) {
					start = false;
					break;
				}
			}
		}

		if (start) {
			SceneTransitioner.instance.LoadNext("LevelSelect");
		}
	}

	bool CompareColor(int player) {
    	for (int i = 0; i < currentMat.Length; ++i) {
    		if (inGame[i]) {
				if (i != player && currentMat[player] == currentMat[i]) return true;
    		}
    	}
        return false;
    }

	void GetInput(int playerNum) {
		Controller controller = controllers[playerNum];
		if (inGame[playerNum]) {//color shit here
			int colorOffset = 0;
			if (controller.GetHandActionDown(0) && !pReady[playerNum]) {
				AudioManager.instance.PlayClip(AudioManager.instance.tick);
				colorOffset = -1;
			}
			else if (controller.GetHandActionDown(1) && !pReady[playerNum]) {
				AudioManager.instance.PlayClip(AudioManager.instance.tick);
				colorOffset = 1;
			}

			if (colorOffset != 0) {
				currentMat[playerNum] = (currentMat[playerNum] + colorOffset + materials.Length) % materials.Length;
				while (CompareColor(playerNum)) {
					currentMat[playerNum] = (currentMat[playerNum] + colorOffset + materials.Length) % materials.Length;
				}
				foreach (Renderer r in Joined[playerNum].GetComponentsInChildren<Renderer>()) {
					r.material = materials[currentMat[playerNum]].mat;
				}
				GlobalPlayerManager.inst.SetMaterial(playerNum, materials[currentMat[playerNum]]);
			}
			else if (controller.GetConfirmDown() && !pReady[playerNum]) {
				pReady[playerNum] = true;
				playerReady[playerNum].SetActive(true);
				JoinedUI[playerNum].SetActive(false);
				AudioManager.instance.PlayClip(AudioManager.instance.confirm);
			}
			else if (controller.GetBackDown() && pReady[playerNum]) {
				pReady[playerNum] = false;
				playerReady[playerNum].SetActive(false);
				JoinedUI[playerNum].SetActive(true);
				AudioManager.instance.PlayClip(AudioManager.instance.back);
			} 
			else if (controller.GetBackDown() && !pReady[playerNum]) {
				inGame[playerNum] = false;
				GlobalPlayerManager.inst.SetInGameFalse(playerNum);
				Join[playerNum].SetActive(true);
				Joined[playerNum].SetActive(false);
				JoinedUI[playerNum].SetActive(false);
				numPlayers--;
				//currentMat[playerNum] = 0;
				AudioManager.instance.PlayClip(AudioManager.instance.back);
			}
		}
		else {
			if (controller.GetConfirmDown()) {
				AudioManager.instance.PlayClip(AudioManager.instance.confirm);
				inGame[playerNum] = true;
				GlobalPlayerManager.inst.SetInGameTrue(playerNum);
				Join[playerNum].SetActive(false);
				Joined[playerNum].SetActive(true);
				JoinedUI[playerNum].SetActive(true);
				numPlayers++;
				while (CompareColor(playerNum)) {
					currentMat[playerNum]++;
					if (currentMat[playerNum] > 9) currentMat[playerNum] = 0;
				}
				foreach (Renderer r in Joined[playerNum].GetComponentsInChildren<Renderer>()) {
					r.material = materials[currentMat[playerNum]].mat;
				}
				GlobalPlayerManager.inst.SetMaterial(playerNum, materials[currentMat[playerNum]]);
			}
			else if (controller.GetBackDown()) {
				bool noOneInGame = true;
				foreach (bool b in inGame) {
					if (b) {
						noOneInGame = false;
						break;
					}
				}

				if (noOneInGame) {
					SceneTransitioner.instance.LoadBack("Title Screen");
				}
			}
		}
	}

	public static void AddToGame(int i) {
		inGame[i] = true;
	}
}
