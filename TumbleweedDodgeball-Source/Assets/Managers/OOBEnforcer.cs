using UnityEngine;
using System.Collections;

public class OOBEnforcer : MonoBehaviour {

	void OnTriggerStay2D(Collider2D coll) {
		if (coll.tag != "Player" || coll.tag != "Ball") {
			Transform root = coll.transform.root.transform;
			root.position += -root.transform.position.normalized * 7f;
		}

	}
}