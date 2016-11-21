using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour {

	private int curSelect = 0;

	public Material[] materials;
	public int[] currentMat;

	public GameObject[] Join;
	public GameObject[] Joined;
	public GameObject[] playerReady;

	private bool[] inGame = new bool[4];
	private bool[] pReady = new bool[4];

	private int numPlayers;
	
	void Start () {
		for (int i = 0; i < inGame.Length; ++i) {
			inGame[i] = false;
			pReady[i] = false;
		}
	}

	void Update () {
		//p1
		if (inGame[0]) {//color shit here
			if (Input.GetKeyDown(KeyCode.Q) && !pReady[0]) { //playerReady
	            currentMat[0]--;
	            if (currentMat[0] < 0) currentMat[0] = 9;
	            while (CompareColor(0)) {
	                currentMat[0]--;
	                if (currentMat[0] < 0) currentMat[0] = 9;
	            }
				foreach (MeshRenderer r in Joined[0].GetComponentsInChildren<MeshRenderer>()) {
	                r.material = materials[currentMat[0]];
				}
				GlobalPlayerManager.inst.SetMaterial(0,materials[currentMat[0]]);
	        }
	        else if (Input.GetKeyDown(KeyCode.W) && !pReady[0]) {
	            currentMat[0]++;
	            if (currentMat[0] > 9) currentMat[0] = 0;
	            while (CompareColor(0)) {
	                currentMat[0]++;
	                if (currentMat[0] > 9) currentMat[0] = 0;
	            }
				foreach (MeshRenderer r in Joined[0].GetComponentsInChildren<MeshRenderer>()) {
	                r.material = materials[currentMat[0]];
				}
				GlobalPlayerManager.inst.SetMaterial(0,materials[currentMat[0]]);
	        }
	        else if (Input.GetKeyDown(KeyCode.A)) {
				pReady[0] = true;
				playerReady[0].SetActive(true);
	        }
	        else if (Input.GetKeyDown(KeyCode.S)) {
				pReady[0] = false;
				playerReady[0].SetActive(false);
	        }
		} else {
			if (Input.GetKeyDown(KeyCode.A)) {
				inGame[0] = true;
				Join[0].SetActive(false);
				Joined[0].SetActive(true);
				numPlayers++;
				while (CompareColor(0)) {
	                currentMat[0]++;
	                if (currentMat[0] > 9) currentMat[0] = 0;
	            }
				foreach (MeshRenderer r in Joined[0].GetComponentsInChildren<MeshRenderer>()) {
	                r.material = materials[currentMat[0]];
				}
				GlobalPlayerManager.inst.SetMaterial(0,materials[currentMat[0]]);
			}
		}
		//p2
		if (inGame[1]) {//color shit here
			if (Input.GetKeyDown(KeyCode.E) && !pReady[1]) { //playerReady
	            currentMat[1]--;
	            if (currentMat[1] < 0) currentMat[1] = 9;
	            while (CompareColor(1)) {
	                currentMat[1]--;
	                if (currentMat[1] < 0) currentMat[1] = 9;
	            }
				foreach (MeshRenderer r in Joined[1].GetComponentsInChildren<MeshRenderer>()) {
	                r.material = materials[currentMat[1]];
				}
				GlobalPlayerManager.inst.SetMaterial(1,materials[currentMat[1]]);
	        }
	        else if (Input.GetKeyDown(KeyCode.R) && !pReady[1]) {
	            currentMat[1]++;
	            if (currentMat[1] > 9) currentMat[1] = 0;
	            while (CompareColor(1)) {
	                currentMat[1]++;
	                if (currentMat[1] > 9) currentMat[1] = 0;
	            }
				foreach (MeshRenderer r in Joined[1].GetComponentsInChildren<MeshRenderer>()) {
	                r.material = materials[currentMat[1]];
				}
				GlobalPlayerManager.inst.SetMaterial(1,materials[currentMat[1]]);
	        }
	        else if (Input.GetKeyDown(KeyCode.D)) {
				pReady[1] = true;
				playerReady[1].SetActive(true);
	        }
	        else if (Input.GetKeyDown(KeyCode.F)) {
				pReady[1] = false;
				playerReady[1].SetActive(false);
	        }
		} else {
			if (Input.GetKeyDown(KeyCode.D)) {
				inGame[1] = true;
				Join[1].SetActive(false);
				Joined[1].SetActive(true);
				numPlayers++;
				while (CompareColor(1)) {
	                currentMat[1]++;
	                if (currentMat[1] > 9) currentMat[1] = 0;
	            }
				foreach (MeshRenderer r in Joined[1].GetComponentsInChildren<MeshRenderer>()) {
	                r.material = materials[currentMat[1]];
				}
				GlobalPlayerManager.inst.SetMaterial(1,materials[currentMat[1]]);
			}
		}
		//p3
		if (inGame[2]) {//color shit here
			if (Input.GetKeyDown(KeyCode.T) && !pReady[2]) { //playerReady
	            currentMat[2]--;
	            if (currentMat[2] < 0) currentMat[2] = 9;
	            while (CompareColor(2)) {
	                currentMat[2]--;
	                if (currentMat[2] < 0) currentMat[2] = 9;
	            }
				foreach (MeshRenderer r in Joined[2].GetComponentsInChildren<MeshRenderer>()) {
	                r.material = materials[currentMat[2]];
				}
				GlobalPlayerManager.inst.SetMaterial(2,materials[currentMat[2]]);
	        }
	        else if (Input.GetKeyDown(KeyCode.Y) && !pReady[2]) {
	            currentMat[2]++;
	            if (currentMat[2] > 9) currentMat[2] = 0;
	            while (CompareColor(2)) {
	                currentMat[2]++;
	                if (currentMat[2] > 9) currentMat[2] = 0;
	            }
				foreach (MeshRenderer r in Joined[2].GetComponentsInChildren<MeshRenderer>()) {
	                r.material = materials[currentMat[2]];
				}
				GlobalPlayerManager.inst.SetMaterial(2,materials[currentMat[2]]);
	        }
	        else if (Input.GetKeyDown(KeyCode.H)) {
				pReady[2] = true;
				playerReady[2].SetActive(true);
	        }
	        else if (Input.GetKeyDown(KeyCode.G)) {
				pReady[2] = false;
				playerReady[2].SetActive(false);
	        }
		} else {
			if (Input.GetKeyDown(KeyCode.G)) {
				inGame[2] = true;
				Join[2].SetActive(false);
				Joined[2].SetActive(true);
				numPlayers++;
				while (CompareColor(2)) {
	                currentMat[2]++;
	                if (currentMat[2] > 9) currentMat[2] = 0;
	            }
				foreach (MeshRenderer r in Joined[2].GetComponentsInChildren<MeshRenderer>()) {
	                r.material = materials[currentMat[2]];
				}
				GlobalPlayerManager.inst.SetMaterial(2,materials[currentMat[2]]);
			}
		}
		//p4
		if (inGame[3]) {//color shit here
			if (Input.GetKeyDown(KeyCode.U) && !pReady[3]) { //playerReady
	            currentMat[3]--;
	            if (currentMat[3] < 0) currentMat[3] = 9;
	            while (CompareColor(3)) {
	                currentMat[3]--;
	                if (currentMat[3] < 0) currentMat[3] = 9;
	            }
				foreach (MeshRenderer r in Joined[3].GetComponentsInChildren<MeshRenderer>()) {
	                r.material = materials[currentMat[3]];
				}
				GlobalPlayerManager.inst.SetMaterial(3,materials[currentMat[3]]);
	        }
	        else if (Input.GetKeyDown(KeyCode.I) && !pReady[3]) {
	            currentMat[3]++;
	            if (currentMat[3] > 9) currentMat[3] = 0;
	            while (CompareColor(3)) {
	                currentMat[3]++;
	                if (currentMat[3] > 9) currentMat[3] = 0;
	            }
				foreach (MeshRenderer r in Joined[3].GetComponentsInChildren<MeshRenderer>()) {
	                r.material = materials[currentMat[3]];
				}
				GlobalPlayerManager.inst.SetMaterial(3,materials[currentMat[3]]);
	        }
	        else if (Input.GetKeyDown(KeyCode.K)) {
				pReady[3] = true;
				playerReady[3].SetActive(true);
	        }
	        else if (Input.GetKeyDown(KeyCode.J)) {
				pReady[3] = false;
				playerReady[3].SetActive(false);
	        }
		} else {
			if (Input.GetKeyDown(KeyCode.J)) {
				inGame[3] = true;
				Join[3].SetActive(false);
				Joined[3].SetActive(true);
				numPlayers++;
				while (CompareColor(3)) {
	                currentMat[3]++;
	                if (currentMat[3] > 9) currentMat[3] = 0;
	            }
				foreach (MeshRenderer r in Joined[3].GetComponentsInChildren<MeshRenderer>()) {
	                r.material = materials[currentMat[3]];
				}
				GlobalPlayerManager.inst.SetMaterial(3,materials[currentMat[3]]);
			}
		}
		bool allReady = true;
		int readyCount = 0;
		for (int i = 0; i < 4; ++i) {
			if (inGame[i]) {
				if (pReady[i]) {
					readyCount++;
				}
				else {
					allReady = false;
				}
			}
		}
		if (allReady && readyCount >= 2){
			GlobalPlayerManager.inst.SetNumPlayers(readyCount);
			SceneManager.LoadScene("Cliffside_npst");
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
}
