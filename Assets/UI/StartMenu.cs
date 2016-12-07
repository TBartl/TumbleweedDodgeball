using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using InControl;

public class StartMenu : MonoBehaviour {

	private int curSelect = 0;

	public PlayerColor[] materials;
	public int[] currentMat;

	public GameObject[] Join;
	public GameObject[] Joined;
	public GameObject[] playerReady;

	private bool[] inGame = new bool[4];
	private bool[] pReady = new bool[4];

	private int numPlayers;

	private Controller[] controllers;

	public GameObject controllerPrefab;
	
	void Start () {
		controllers = new Controller[inGame.Length];
		for (int i = 0; i < inGame.Length; ++i) {
			inGame[i] = false;
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

		//for (int i = 0; i < inGame.Length; ++i) {
		//	if (inGame[i] && !pReady[i]) {
		//		start = false;
		//	}
		//}

		if (start) {
			//SceneManager.LoadScene("Levels/Cliffside");
			SceneTransitioner.instance.LoadScene("LevelSelect");
		}



		//if (inGame[0]) {//color shit here
		//	if (Input.GetKeyDown(KeyCode.Q) && !pReady[0]) { //playerReady
		//           currentMat[0]--;
		//           if (currentMat[0] < 0) currentMat[0] = 9;
		//           while (CompareColor(0)) {
		//               currentMat[0]--;
		//               if (currentMat[0] < 0) currentMat[0] = 9;
		//           }
		//		foreach (MeshRenderer r in Joined[0].GetComponentsInChildren<MeshRenderer>()) {
		//               r.material = materials[currentMat[0]];
		//		}
		//		GlobalPlayerManager.inst.SetMaterial(0,materials[currentMat[0]]);
		//       }
		//       else if (Input.GetKeyDown(KeyCode.W) && !pReady[0]) {
		//           currentMat[0]++;
		//           if (currentMat[0] > 9) currentMat[0] = 0;
		//           while (CompareColor(0)) {
		//               currentMat[0]++;
		//               if (currentMat[0] > 9) currentMat[0] = 0;
		//           }
		//		foreach (MeshRenderer r in Joined[0].GetComponentsInChildren<MeshRenderer>()) {
		//               r.material = materials[currentMat[0]];
		//		}
		//		GlobalPlayerManager.inst.SetMaterial(0,materials[currentMat[0]]);
		//       }
		//       else if (Input.GetKeyDown(KeyCode.A)) {
		//		pReady[0] = true;
		//		playerReady[0].SetActive(true);
		//       }
		//       else if (Input.GetKeyDown(KeyCode.S)) {
		//		pReady[0] = false;
		//		playerReady[0].SetActive(false);
		//       }
		//}
		//else {
		//	if (Input.GetKeyDown(KeyCode.A)) {
		//		inGame[0] = true;
		//		Join[0].SetActive(false);
		//		Joined[0].SetActive(true);
		//		numPlayers++;
		//		while (CompareColor(0)) {
		//               currentMat[0]++;
		//               if (currentMat[0] > 9) currentMat[0] = 0;
		//           }
		//		foreach (MeshRenderer r in Joined[0].GetComponentsInChildren<MeshRenderer>()) {
		//               r.material = materials[currentMat[0]];
		//		}
		//		GlobalPlayerManager.inst.SetMaterial(0,materials[currentMat[0]]);
		//	}
		//}





		////p2
		//if (inGame[1]) {//color shit here
		//	if (Input.GetKeyDown(KeyCode.E) && !pReady[1]) { //playerReady
		//           currentMat[1]--;
		//           if (currentMat[1] < 0) currentMat[1] = 9;
		//           while (CompareColor(1)) {
		//               currentMat[1]--;
		//               if (currentMat[1] < 0) currentMat[1] = 9;
		//           }
		//		foreach (MeshRenderer r in Joined[1].GetComponentsInChildren<MeshRenderer>()) {
		//               r.material = materials[currentMat[1]];
		//		}
		//		GlobalPlayerManager.inst.SetMaterial(1,materials[currentMat[1]]);
		//       }
		//       else if (Input.GetKeyDown(KeyCode.R) && !pReady[1]) {
		//           currentMat[1]++;
		//           if (currentMat[1] > 9) currentMat[1] = 0;
		//           while (CompareColor(1)) {
		//               currentMat[1]++;
		//               if (currentMat[1] > 9) currentMat[1] = 0;
		//           }
		//		foreach (MeshRenderer r in Joined[1].GetComponentsInChildren<MeshRenderer>()) {
		//               r.material = materials[currentMat[1]];
		//		}
		//		GlobalPlayerManager.inst.SetMaterial(1,materials[currentMat[1]]);
		//       }
		//       else if (Input.GetKeyDown(KeyCode.D)) {
		//		pReady[1] = true;
		//		playerReady[1].SetActive(true);
		//       }
		//       else if (Input.GetKeyDown(KeyCode.F)) {
		//		pReady[1] = false;
		//		playerReady[1].SetActive(false);
		//       }
		//} else {
		//	if (Input.GetKeyDown(KeyCode.D)) {
		//		inGame[1] = true;
		//		Join[1].SetActive(false);
		//		Joined[1].SetActive(true);
		//		numPlayers++;
		//		while (CompareColor(1)) {
		//               currentMat[1]++;
		//               if (currentMat[1] > 9) currentMat[1] = 0;
		//           }
		//		foreach (MeshRenderer r in Joined[1].GetComponentsInChildren<MeshRenderer>()) {
		//               r.material = materials[currentMat[1]];
		//		}
		//		GlobalPlayerManager.inst.SetMaterial(1,materials[currentMat[1]]);
		//	}
		//}
		////p3
		//if (inGame[2]) {//color shit here
		//	if (Input.GetKeyDown(KeyCode.T) && !pReady[2]) { //playerReady
		//           currentMat[2]--;
		//           if (currentMat[2] < 0) currentMat[2] = 9;
		//           while (CompareColor(2)) {
		//               currentMat[2]--;
		//               if (currentMat[2] < 0) currentMat[2] = 9;
		//           }
		//		foreach (MeshRenderer r in Joined[2].GetComponentsInChildren<MeshRenderer>()) {
		//               r.material = materials[currentMat[2]];
		//		}
		//		GlobalPlayerManager.inst.SetMaterial(2,materials[currentMat[2]]);
		//       }
		//       else if (Input.GetKeyDown(KeyCode.Y) && !pReady[2]) {
		//           currentMat[2]++;
		//           if (currentMat[2] > 9) currentMat[2] = 0;
		//           while (CompareColor(2)) {
		//               currentMat[2]++;
		//               if (currentMat[2] > 9) currentMat[2] = 0;
		//           }
		//		foreach (MeshRenderer r in Joined[2].GetComponentsInChildren<MeshRenderer>()) {
		//               r.material = materials[currentMat[2]];
		//		}
		//		GlobalPlayerManager.inst.SetMaterial(2,materials[currentMat[2]]);
		//       }
		//       else if (Input.GetKeyDown(KeyCode.G)) {
		//		pReady[2] = true;
		//		playerReady[2].SetActive(true);
		//       }
		//       else if (Input.GetKeyDown(KeyCode.H)) {
		//		pReady[2] = false;
		//		playerReady[2].SetActive(false);
		//       }
		//} else {
		//	if (Input.GetKeyDown(KeyCode.G)) {
		//		inGame[2] = true;
		//		Join[2].SetActive(false);
		//		Joined[2].SetActive(true);
		//		numPlayers++;
		//		while (CompareColor(2)) {
		//               currentMat[2]++;
		//               if (currentMat[2] > 9) currentMat[2] = 0;
		//           }
		//		foreach (MeshRenderer r in Joined[2].GetComponentsInChildren<MeshRenderer>()) {
		//               r.material = materials[currentMat[2]];
		//		}
		//		GlobalPlayerManager.inst.SetMaterial(2,materials[currentMat[2]]);
		//	}
		//}
		////p4
		//if (inGame[3]) {//color shit here
		//	if (Input.GetKeyDown(KeyCode.U) && !pReady[3]) { //playerReady
		//           currentMat[3]--;
		//           if (currentMat[3] < 0) currentMat[3] = 9;
		//           while (CompareColor(3)) {
		//               currentMat[3]--;
		//               if (currentMat[3] < 0) currentMat[3] = 9;
		//           }
		//		foreach (MeshRenderer r in Joined[3].GetComponentsInChildren<MeshRenderer>()) {
		//               r.material = materials[currentMat[3]];
		//		}
		//		GlobalPlayerManager.inst.SetMaterial(3,materials[currentMat[3]]);
		//       }
		//       else if (Input.GetKeyDown(KeyCode.I) && !pReady[3]) {
		//           currentMat[3]++;
		//           if (currentMat[3] > 9) currentMat[3] = 0;
		//           while (CompareColor(3)) {
		//               currentMat[3]++;
		//               if (currentMat[3] > 9) currentMat[3] = 0;
		//           }
		//		foreach (MeshRenderer r in Joined[3].GetComponentsInChildren<MeshRenderer>()) {
		//               r.material = materials[currentMat[3]];
		//		}
		//		GlobalPlayerManager.inst.SetMaterial(3,materials[currentMat[3]]);
		//       }
		//       else if (Input.GetKeyDown(KeyCode.J)) {
		//		pReady[3] = true;
		//		playerReady[3].SetActive(true);
		//       }
		//       else if (Input.GetKeyDown(KeyCode.K)) {
		//		pReady[3] = false;
		//		playerReady[3].SetActive(false);
		//       }
		//} else {
		//	if (Input.GetKeyDown(KeyCode.J)) {
		//		inGame[3] = true;
		//		Join[3].SetActive(false);
		//		Joined[3].SetActive(true);
		//		numPlayers++;
		//		while (CompareColor(3)) {
		//               currentMat[3]++;
		//               if (currentMat[3] > 9) currentMat[3] = 0;
		//           }
		//		foreach (MeshRenderer r in Joined[3].GetComponentsInChildren<MeshRenderer>()) {
		//               r.material = materials[currentMat[3]];
		//		}
		//		GlobalPlayerManager.inst.SetMaterial(3,materials[currentMat[3]]);
		//	}
		//}
		//bool allReady = true;
		//int readyCount = 0;
		//for (int i = 0; i < 4; ++i) {
		//	if (inGame[i]) {
		//		if (pReady[i]) {
		//			readyCount++;
		//		}
		//		else {
		//			allReady = false;
		//		}
		//	}
		//}
		//if (allReady && readyCount >= 2){
		//	GlobalPlayerManager.inst.SetNumPlayers(readyCount - 1);
		//	SceneManager.LoadScene("Cliffside_npst");
		//}
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
				AudioManager.instance.PlayClip(AudioManager.instance.confirm);
			}
			else if (controller.GetBackDown() && pReady[playerNum]) {
				pReady[playerNum] = false;
				playerReady[playerNum].SetActive(false);
				AudioManager.instance.PlayClip(AudioManager.instance.back);
			} 
			else if (controller.GetBackDown() && !pReady[playerNum]) {
				inGame[playerNum] = false;
				GlobalPlayerManager.inst.SetInGameFalse(playerNum);
				Join[playerNum].SetActive(true);
				Joined[playerNum].SetActive(false);
				numPlayers--;
				currentMat[playerNum] = 0;
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
		}
	}
}
