using UnityEngine;
using System.Collections;

public class DropItemOnHide : OnHide {

	public GameObject itemPrefab;
	public float probability;

	public override void Hide() {
		if (Random.Range(0f, 1f) >= probability) {
			if (!DebugManager.useRandomPrefabs)
				Instantiate(itemPrefab, transform.position, Quaternion.identity);
			else
				Instantiate(DebugManager.inst.randomPrefabs[Random.Range(0,DebugManager.inst.randomPrefabs.Count)], transform.position, Quaternion.identity);
		}
	}
}
