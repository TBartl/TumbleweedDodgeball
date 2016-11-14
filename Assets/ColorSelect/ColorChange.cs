using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ColorChange : MonoBehaviour {

	public Material[] materials;
	public int[] currentMat;

	private bool p1Ready = false, p2Ready = false, p3Ready = false, p4Ready = false;

	public GameObject[] playerModels2;
	public GameObject[] playerModels3;
	public GameObject[] playerModels4;

	public GameObject[] playerReady2;
	public GameObject[] playerReady3;
	public GameObject[] playerReady4;

	public GameObject[] playerText2;
	public GameObject[] playerText3;
	public GameObject[] playerText4;

	private int numPlayers;

	void Start () {

		numPlayers = NumPlayers.inst.GetNumPlayers();

		for (int i = 0; i < playerReady2.Length; ++i) {
			playerReady2[i].SetActive(false);
		}
		for (int i = 0; i < playerReady3.Length; ++i) {
			playerReady3[i].SetActive(false);
		}
		for (int i = 0; i < playerReady4.Length; ++i) {
			playerReady4[i].SetActive(false);
		}

		if (numPlayers != 2) {
			for (int i = 0; i < playerText2.Length; ++i) {
				playerText2[i].SetActive(false);
			}
			for (int i = 0; i < playerModels2.Length; ++i) {
				playerModels2[i].SetActive(false);
			}
		}
		if (numPlayers != 3) {
			for (int i = 0; i < playerText3.Length; ++i) {
				playerText3[i].SetActive(false);
			}
			for (int i = 0; i < playerModels3.Length; ++i) {
				playerModels3[i].SetActive(false);
			}
		}
		if (numPlayers != 4) {
			for (int i = 0; i < playerText4.Length; ++i) {
				playerText4[i].SetActive(false);
			}
			for (int i = 0; i < playerModels4.Length; ++i) {
				playerModels4[i].SetActive(false);
			}
		}

	}

	void Update () {
        //p1
		if (Input.GetKeyDown(KeyCode.Q) && !p1Ready) { //playerReady
            currentMat[0]--;
            if (currentMat[0] < 0) currentMat[0] = 9;
            while (CompareColor(0)) {
                currentMat[0]--;
                if (currentMat[0] < 0) currentMat[0] = 9;
            }
			switch(numPlayers) {
	            case 2:
					foreach (MeshRenderer r in playerModels2[0].GetComponentsInChildren<MeshRenderer>()) {
		                r.material = materials[currentMat[0]];
					}
					break;
	            case 3:
					foreach (MeshRenderer r in playerModels3[0].GetComponentsInChildren<MeshRenderer>()) {
		                r.material = materials[currentMat[0]];
					}
					break;
	            case 4:
					foreach (MeshRenderer r in playerModels4[0].GetComponentsInChildren<MeshRenderer>()) {
		                r.material = materials[currentMat[0]];
					}
					break;
	        }
			NumPlayers.inst.SetMaterial(0,materials[currentMat[0]]);
        }
        else if (Input.GetKeyDown(KeyCode.W) && !p1Ready) {
            currentMat[0]++;
            if (currentMat[0] > 9) currentMat[0] = 0;
            while (CompareColor(0)) {
                currentMat[0]++;
                if (currentMat[0] > 9) currentMat[0] = 0;
            }
			switch(numPlayers) {
	            case 2:
					foreach (MeshRenderer r in playerModels2[0].GetComponentsInChildren<MeshRenderer>()) {
		                r.material = materials[currentMat[0]];
					}
					break;
	            case 3:
					foreach (MeshRenderer r in playerModels3[0].GetComponentsInChildren<MeshRenderer>()) {
		                r.material = materials[currentMat[0]];
					}
					break;
	            case 4:
					foreach (MeshRenderer r in playerModels4[0].GetComponentsInChildren<MeshRenderer>()) {
		                r.material = materials[currentMat[0]];
					}
					break;
	        }
			NumPlayers.inst.SetMaterial(0,materials[currentMat[0]]);
        }
        else if (Input.GetKeyDown(KeyCode.A)) {
            p1Ready = true;
			switch(numPlayers) {
	            case 2:
					playerReady2[0].SetActive(true);
					break;
	            case 3:
					playerReady3[0].SetActive(true);
					break;
	            case 4:
					playerReady4[0].SetActive(true);
					break;
	        }
        }
        else if (Input.GetKeyDown(KeyCode.S)) {
            p1Ready = false;
			switch(numPlayers) {
	            case 2:
					playerReady2[0].SetActive(false);
					break;
	            case 3:
					playerReady3[0].SetActive(false);
					break;
	            case 4:
					playerReady4[0].SetActive(false);
					break;
	        }
        }

        //p2
		if (Input.GetKeyDown(KeyCode.E) && !p2Ready) { //playerReady
            currentMat[1]--;
            if (currentMat[1] < 0) currentMat[1] = 9;
            while (CompareColor(1)) {
                currentMat[1]--;
                if (currentMat[1] < 0) currentMat[1] = 9;
            }
			switch(numPlayers) {
	            case 2:
					foreach (MeshRenderer r in playerModels2[1].GetComponentsInChildren<MeshRenderer>()) {
		                r.material = materials[currentMat[1]];
					}
					break;
	            case 3:
					foreach (MeshRenderer r in playerModels3[1].GetComponentsInChildren<MeshRenderer>()) {
		                r.material = materials[currentMat[1]];
					}
					break;
	            case 4:
					foreach (MeshRenderer r in playerModels4[1].GetComponentsInChildren<MeshRenderer>()) {
		                r.material = materials[currentMat[1]];
					}
					break;
	        }
			NumPlayers.inst.SetMaterial(1,materials[currentMat[1]]);
        }
        else if (Input.GetKeyDown(KeyCode.R) && !p2Ready) {
            currentMat[1]++;
            if (currentMat[1] > 9) currentMat[1] = 0;
            while (CompareColor(1)) {
                currentMat[1]++;
                if (currentMat[1] > 9) currentMat[1] = 0;
            }
			switch(numPlayers) {
	            case 2:
					foreach (MeshRenderer r in playerModels2[1].GetComponentsInChildren<MeshRenderer>()) {
		                r.material = materials[currentMat[1]];
					}
					break;
	            case 3:
					foreach (MeshRenderer r in playerModels3[1].GetComponentsInChildren<MeshRenderer>()) {
		                r.material = materials[currentMat[1]];
					}
					break;
	            case 4:
					foreach (MeshRenderer r in playerModels4[1].GetComponentsInChildren<MeshRenderer>()) {
		                r.material = materials[currentMat[1]];
					}
					break;
	        }
			NumPlayers.inst.SetMaterial(1,materials[currentMat[1]]);
        }
        else if (Input.GetKeyDown(KeyCode.D)) {
            p2Ready = true;
			switch(numPlayers) {
	            case 2:
					playerReady2[1].SetActive(true);
					break;
	            case 3:
					playerReady3[1].SetActive(true);
					break;
	            case 4:
					playerReady4[1].SetActive(true);
					break;
	        }
        }
        else if (Input.GetKeyDown(KeyCode.F)) {
            p2Ready = false;
			switch(numPlayers) {
	            case 2:
					playerReady2[1].SetActive(false);
					break;
	            case 3:
					playerReady3[1].SetActive(false);
					break;
	            case 4:
					playerReady4[1].SetActive(false);
					break;
	        }
        }

        //p3
        if (numPlayers >= 3) {
			if (Input.GetKeyDown(KeyCode.T) && !p3Ready) { //playerReady
	            currentMat[2]--;
	            if (currentMat[2] < 0) currentMat[2] = 9;
	            while (CompareColor(2)) {
	                currentMat[2]--;
	                if (currentMat[2] < 0) currentMat[2] = 9;
	            }
				switch(numPlayers) {
		            case 2:
						foreach (MeshRenderer r in playerModels2[2].GetComponentsInChildren<MeshRenderer>()) {
			                r.material = materials[currentMat[2]];
						}
						break;
		            case 3:
						foreach (MeshRenderer r in playerModels3[2].GetComponentsInChildren<MeshRenderer>()) {
			                r.material = materials[currentMat[2]];
						}
						break;
		            case 4:
						foreach (MeshRenderer r in playerModels4[2].GetComponentsInChildren<MeshRenderer>()) {
			                r.material = materials[currentMat[2]];
						}
						break;
		        }
				NumPlayers.inst.SetMaterial(2,materials[currentMat[2]]);
	        }
	        else if (Input.GetKeyDown(KeyCode.Y) && !p3Ready) {
	            currentMat[2]++;
	            if (currentMat[2] > 9) currentMat[2] = 0;
	            while (CompareColor(2)) {
	                currentMat[2]++;
	                if (currentMat[2] > 9) currentMat[2] = 0;
	            }
				switch(numPlayers) {
		            case 2:
						foreach (MeshRenderer r in playerModels2[2].GetComponentsInChildren<MeshRenderer>()) {
			                r.material = materials[currentMat[2]];
						}
						break;
		            case 3:
						foreach (MeshRenderer r in playerModels3[2].GetComponentsInChildren<MeshRenderer>()) {
			                r.material = materials[currentMat[2]];
						}
						break;
		            case 4:
						foreach (MeshRenderer r in playerModels4[2].GetComponentsInChildren<MeshRenderer>()) {
			                r.material = materials[currentMat[2]];
						}
						break;
		        }
				NumPlayers.inst.SetMaterial(2,materials[currentMat[2]]);
	        }
	        else if (Input.GetKeyDown(KeyCode.G)) {//readies p3
	            p3Ready = true;
				switch(numPlayers) {
		            case 2:
						playerReady2[2].SetActive(true);
						break;
		            case 3:
						playerReady3[2].SetActive(true);
						break;
		            case 4:
						playerReady4[2].SetActive(true);
						break;
		        }
	        }
	        else if (Input.GetKeyDown(KeyCode.H)) {//unreadies p3
	            p3Ready = false;
				switch(numPlayers) {
		            case 2:
						playerReady2[2].SetActive(false);
						break;
		            case 3:
						playerReady3[2].SetActive(false);
						break;
		            case 4:
						playerReady4[2].SetActive(false);
						break;
		        }
	        }
        }

        //p4
        if (numPlayers >= 4){
			if (Input.GetKeyDown(KeyCode.U) && !p4Ready) { //playerReady
	            currentMat[3]--;
	            if (currentMat[3] < 0) currentMat[3] = 9;
	            while (CompareColor(3)) {
	                currentMat[3]--;
	                if (currentMat[3] < 0) currentMat[3] = 9;
	            }
				switch(numPlayers) {
		            case 2:
						foreach (MeshRenderer r in playerModels2[3].GetComponentsInChildren<MeshRenderer>()) {
			                r.material = materials[currentMat[3]];
						}
						break;
		            case 3:
						foreach (MeshRenderer r in playerModels3[3].GetComponentsInChildren<MeshRenderer>()) {
			                r.material = materials[currentMat[3]];
						}
						break;
		            case 4:
						foreach (MeshRenderer r in playerModels4[3].GetComponentsInChildren<MeshRenderer>()) {
			                r.material = materials[currentMat[3]];
						}
						break;
		        }
				NumPlayers.inst.SetMaterial(3,materials[currentMat[3]]);
	        }
	        else if (Input.GetKeyDown(KeyCode.I) && !p4Ready) {
	            currentMat[3]++;
	            if (currentMat[3] > 9) currentMat[3] = 0;
	            while (CompareColor(3)) {
	                currentMat[3]++;
	                if (currentMat[3] > 9) currentMat[3] = 0;
	            }
				switch(numPlayers) {
		            case 2:
						foreach (MeshRenderer r in playerModels2[3].GetComponentsInChildren<MeshRenderer>()) {
			                r.material = materials[currentMat[3]];
						}
						break;
		            case 3:
						foreach (MeshRenderer r in playerModels3[3].GetComponentsInChildren<MeshRenderer>()) {
			                r.material = materials[currentMat[3]];
						}
						break;
		            case 4:
						foreach (MeshRenderer r in playerModels4[3].GetComponentsInChildren<MeshRenderer>()) {
			                r.material = materials[currentMat[3]];
						}
						break;
		        }
				NumPlayers.inst.SetMaterial(3,materials[currentMat[3]]);
	        }
	        else if (Input.GetKeyDown(KeyCode.J)) {
	            p4Ready = true;
				switch(numPlayers) {
		            case 2:
						playerReady2[3].SetActive(true);
						break;
		            case 3:
						playerReady3[3].SetActive(true);
						break;
		            case 4:
						playerReady4[3].SetActive(true);
						break;
		        }
	        }
	        else if (Input.GetKeyDown(KeyCode.K)) {
	            p4Ready = false;
				switch(numPlayers) {
		            case 2:
						playerReady2[3].SetActive(false);
						break;
		            case 3:
						playerReady3[3].SetActive(false);
						break;
		            case 4:
						playerReady4[3].SetActive(false);
						break;
		        }
	        }
		}
		if (numPlayers == 2) {
			if (p1Ready && p2Ready) {
				SceneManager.LoadScene("main_npst");
			}
		}
		if (numPlayers == 3) {
			if (p1Ready && p2Ready && p3Ready) {
				SceneManager.LoadScene("main_npst");
			}
		}
		if (numPlayers == 4) {
			if (p1Ready && p2Ready && p3Ready && p4Ready) {
				SceneManager.LoadScene("main_npst");
			}
		}
	}

    bool CompareColor(int player) {
    	if (numPlayers == 4){
	        switch(player) {
	            case 0:
	                return (currentMat[0] == currentMat[1] || currentMat[0] == currentMat[2] || currentMat[0] == currentMat[3]);
	            case 1:
	                return (currentMat[1] == currentMat[0] || currentMat[1] == currentMat[2] || currentMat[1] == currentMat[3]);
	            case 2:
	                return (currentMat[2] == currentMat[1] || currentMat[2] == currentMat[0] || currentMat[2] == currentMat[3]);
	            case 3:
	                return (currentMat[3] == currentMat[0] || currentMat[3] == currentMat[1] || currentMat[3] == currentMat[2]);
	            default:
	                return false;
	        }
        } 
		if (numPlayers == 3){
	        switch(player) {
	            case 0:
	                return (currentMat[0] == currentMat[1] || currentMat[0] == currentMat[2]);
	            case 1:
	                return (currentMat[1] == currentMat[0] || currentMat[1] == currentMat[2]);
	            case 2:
	                return (currentMat[2] == currentMat[1] || currentMat[2] == currentMat[0]);
	            default:
	                return false;
	        }
        }
		if (numPlayers == 2){
	        switch(player) {
	            case 0:
	                return (currentMat[0] == currentMat[1]);
	            case 1:
	                return (currentMat[1] == currentMat[0]);
	            default:
	                return false;
	        }
        }
        return false;
    }
}
