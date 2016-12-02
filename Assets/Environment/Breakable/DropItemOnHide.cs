﻿using UnityEngine;
using System.Collections;

public class DropItemOnHide : OnHide {

	public GameObject itemPrefab;
	public float probability;
	GameObject item;
	bool itemStillThere = false;
	ExplodeAndRespawnOnHit respawner;
	Vector3 startPos;

	void Start() {
		respawner = GetComponent<ExplodeAndRespawnOnHit>();
	}

	void Update() {
		if (itemStillThere) {
			if (item == null || Vector2.Distance(item.transform.position,startPos) > 1) { // object was destroyed or moved
				itemStillThere = false;
				respawner.Respawn();
			}
		}
	}

	public override void Hide() {
		if (Random.Range(0f, 1f) <= probability) {
			if (!DebugManager.useRandomPrefabs)
				item = (GameObject) Instantiate(itemPrefab, transform.position, Quaternion.identity);
			else
				item = (GameObject) Instantiate(DebugManager.inst.randomPrefabs[Random.Range(0,DebugManager.inst.randomPrefabs.Count)], transform.position, Quaternion.identity);
			itemStillThere = true;
			startPos = item.transform.position;
			respawner.NoRespawn();
		}
	}
}
