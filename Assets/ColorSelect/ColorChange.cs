using UnityEngine;
using System.Collections;

public class ColorChange : MonoBehaviour {

	public Material[] materials;
	public int[] currentMat;
	public GameObject[] playerModels;
	public GameObject[] playerReady;
	private bool p1Ready = false, p2Ready = false, p3Ready = false, p4Ready = false;

	void Start () {
		for (int i = 0; i < playerReady.Length; ++i) {
			playerReady[i].SetActive(false);
		}
	}

	void Update () {
        //p1
        updatePlayer(0, p1Ready);

        //p2
        updatePlayer(1, p2Ready);

        //p3
        updatePlayer(2, p3Ready);

        //p4
        updatePlayer(3, p4Ready);

		if (p1Ready && p2Ready && p3Ready && p4Ready) {
			print("all players ready");
		}
	}

    void updatePlayer(int player, bool playerBool) {
        if (Input.GetKeyDown(KeyCode.U) && !playerBool) { //playerReady
            currentMat[player]--;
            if (currentMat[player] < 0) currentMat[player] = 9;
            while (CompareColor(player)) {
                currentMat[player]--;
                if (currentMat[player] < 0) currentMat[player] = 9;
            }
            foreach (MeshRenderer r in playerModels[player].GetComponentsInChildren<MeshRenderer>()) {
                r.material = materials[currentMat[player]];
            }
        }
        else if (Input.GetKeyDown(KeyCode.I) && !playerBool) {
            currentMat[player]++;
            if (currentMat[player] > 9) currentMat[player] = 0;
            while (CompareColor(player)) {
                currentMat[player]++;
                if (currentMat[player] > 9) currentMat[player] = 0;
            }
            foreach (MeshRenderer r in playerModels[player].GetComponentsInChildren<MeshRenderer>()) {
                r.material = materials[currentMat[player]];
            }
        }
        else if (Input.GetKeyDown(KeyCode.J)) {
            playerBool = true;
            playerReady[player].SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.K)) {
            playerBool = false;
            playerReady[player].SetActive(false);
        }
    }

    bool CompareColor(int player) {
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
}
