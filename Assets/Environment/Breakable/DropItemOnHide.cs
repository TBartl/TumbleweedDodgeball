using UnityEngine;
using System.Collections;

public class DropItemOnHide : OnHide {

	public GameObject itemPrefab;
	public float probability;

	public override void Hide() {
		if (Random.Range(0f, 1f) >= probability) {
			GameObject item = Instantiate(itemPrefab);
			item.transform.position = transform.position;
		}
	}
}
