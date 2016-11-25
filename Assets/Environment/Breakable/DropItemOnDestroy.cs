using UnityEngine;
using System.Collections;

public class DropItemOnDestroy : MonoBehaviour {

	public GameObject itemPrefab;
	bool isQuitting = false;

	void OnApplicationQuit() {
		isQuitting = true;
	}

	void OnDestroy() {
		if (!isQuitting) {
			GameObject item = Instantiate(itemPrefab);
			item.transform.position = transform.position;
		}
	}
}
