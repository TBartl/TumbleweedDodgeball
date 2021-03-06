﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerColorizer : MonoBehaviour {
	PlayerData playerData;

	void Awake() {
		playerData = this.GetComponent<PlayerData>();
	}

	void Start () {
		// Things like directional arrow
		foreach (Renderer r in this.GetComponentsInChildren<Renderer>()) {
			r.material.color = PlayerManager.inst.GetColor(playerData.num);
		}

		// Player model
		foreach (MeshRenderer r in this.GetComponentsInChildren<MeshRenderer>()) {
			r.material = PlayerManager.inst.GetMaterial(playerData.num);
		}
		foreach (SkinnedMeshRenderer r in this.GetComponentsInChildren<SkinnedMeshRenderer>()) {
			r.material = PlayerManager.inst.GetMaterial(playerData.num);
		}		
	}

	public void FlashColor(Color c) {
		StartCoroutine(Flash(c));
	}

	IEnumerator Flash(Color c) {
		foreach (SkinnedMeshRenderer r in this.GetComponentsInChildren<SkinnedMeshRenderer>()) {
			r.material.SetColor("_OverColor", c);
		}

		yield return new WaitForSeconds(.1f);


		foreach (SkinnedMeshRenderer r in this.GetComponentsInChildren<SkinnedMeshRenderer>()) {
			r.material = PlayerManager.inst.GetMaterial(playerData.num);
		}
	}
}
