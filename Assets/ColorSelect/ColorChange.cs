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
		if (Input.GetKeyDown(KeyCode.Q) && !p1Ready) {
			currentMat[0]--;
			if (currentMat[0] < 0) currentMat[0] = 9;
			while (currentMat[0] == currentMat[1] || currentMat[0] == currentMat[2] || currentMat[0] == currentMat[3]) {
				currentMat[0]--;
				if (currentMat[0] < 0) currentMat[0] = 9;
			}
			foreach (MeshRenderer r in playerModels[0].GetComponentsInChildren<MeshRenderer>()) {
				r.material = materials[currentMat[0]];
			}
		} else if (Input.GetKeyDown(KeyCode.W) && !p1Ready) {
			currentMat[0]++;
			if (currentMat[0] > 9) currentMat[0] = 0;
			while (currentMat[0] == currentMat[1] || currentMat[0] == currentMat[2] || currentMat[0] == currentMat[3]) {
				currentMat[0]++;
				if (currentMat[0] > 9) currentMat[0] = 0;
			}
			foreach (MeshRenderer r in playerModels[0].GetComponentsInChildren<MeshRenderer>()) {
				r.material = materials[currentMat[0]];
			}
		} else if (Input.GetKeyDown(KeyCode.A)) {
			p1Ready = true;
			playerReady[0].SetActive(true);
		} else if (Input.GetKeyDown(KeyCode.S)) {
			p1Ready = false;
			playerReady[0].SetActive(false);
		}

		//p2
		if (Input.GetKeyDown(KeyCode.E) && !p2Ready) {
			currentMat[1]--;
			if (currentMat[1] < 0) currentMat[1] = 9;
			while (currentMat[1] == currentMat[0] || currentMat[1] == currentMat[2] || currentMat[1] == currentMat[3]) {
				currentMat[1]--;
				if (currentMat[1] < 0) currentMat[1] = 9;
			}
			foreach (MeshRenderer r in playerModels[1].GetComponentsInChildren<MeshRenderer>()) {
				r.material = materials[currentMat[1]];
			}
		} else if (Input.GetKeyDown(KeyCode.R)  && !p2Ready) {
			currentMat[1]++;
			if (currentMat[1] > 9) currentMat[1] = 0;
			while (currentMat[1] == currentMat[0] || currentMat[1] == currentMat[2] || currentMat[1] == currentMat[3]) {
				currentMat[1]++;
				if (currentMat[1] > 9) currentMat[1] = 0;
			}
			foreach (MeshRenderer r in playerModels[1].GetComponentsInChildren<MeshRenderer>()) {
				r.material = materials[currentMat[1]];
			}
		} else if (Input.GetKeyDown(KeyCode.D)) {
			p2Ready = true;
			playerReady[1].SetActive(true);
		} else if (Input.GetKeyDown(KeyCode.F)) {
			p2Ready = false;
			playerReady[1].SetActive(false);
		}

		//p3
		if (Input.GetKeyDown(KeyCode.T) && !p3Ready) {
			currentMat[2]--;
			if (currentMat[2] < 0) currentMat[2] = 9;
			while (currentMat[2] == currentMat[1] || currentMat[2] == currentMat[0] || currentMat[2] == currentMat[3]) {
				currentMat[2]--;
				if (currentMat[2] < 0) currentMat[2] = 9;
			}
			foreach (MeshRenderer r in playerModels[2].GetComponentsInChildren<MeshRenderer>()) {
				r.material = materials[currentMat[2]];
			}
		} else if (Input.GetKeyDown(KeyCode.Y) && !p3Ready) {
			currentMat[2]++;
			if (currentMat[2] > 9) currentMat[2] = 0;
			while (currentMat[2] == currentMat[1] || currentMat[2] == currentMat[0] || currentMat[2] == currentMat[3]) {
				currentMat[2]++;
				if (currentMat[2] > 9) currentMat[2] = 0;
			}
			foreach (MeshRenderer r in playerModels[2].GetComponentsInChildren<MeshRenderer>()) {
				r.material = materials[currentMat[2]];
			}
		} else if (Input.GetKeyDown(KeyCode.G)) {
			p3Ready = true;
			playerReady[2].SetActive(true);
		} else if (Input.GetKeyDown(KeyCode.H)) {
			p3Ready = false;
			playerReady[2].SetActive(false);
		}

		//p4
		if (Input.GetKeyDown(KeyCode.U) && !p4Ready) {
			currentMat[3]--;
			if (currentMat[3] < 0) currentMat[3] = 9;
			while (currentMat[3] == currentMat[0] || currentMat[3] == currentMat[1] || currentMat[3] == currentMat[2]) {
				currentMat[3]--;
				if (currentMat[3] < 0) currentMat[3] = 9;
			}
			foreach (MeshRenderer r in playerModels[3].GetComponentsInChildren<MeshRenderer>()) {
				r.material = materials[currentMat[3]];
			}
		} else if (Input.GetKeyDown(KeyCode.I) && !p4Ready) {
			currentMat[3]++;
			if (currentMat[3] > 9) currentMat[3] = 0;
			while (currentMat[3] == currentMat[0] || currentMat[3] == currentMat[1] || currentMat[3] == currentMat[2]) {
				currentMat[3]++;
				if (currentMat[3] > 9) currentMat[3] = 0;
			}
			foreach (MeshRenderer r in playerModels[3].GetComponentsInChildren<MeshRenderer>()) {
				r.material = materials[currentMat[3]];
			}
		} else if (Input.GetKeyDown(KeyCode.J)) {
			p4Ready = true;
			playerReady[3].SetActive(true);
		} else if (Input.GetKeyDown(KeyCode.K)) {
			p4Ready = false;
			playerReady[3].SetActive(false);
		}

		if (p1Ready && p2Ready && p3Ready && p4Ready) {
			print("all players ready");
		}
	}
}
