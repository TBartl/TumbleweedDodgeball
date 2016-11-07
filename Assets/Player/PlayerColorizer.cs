using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerColorizer : MonoBehaviour {
	PlayerData playerData;

	void Awake() {
		playerData = this.GetComponent<PlayerData>();
	}

	void Start () {
		foreach (Renderer r in this.GetComponentsInChildren<Renderer>()) {
			r.material.color = PlayerManager.inst.GetColor(playerData.num);
		}
	}
}
