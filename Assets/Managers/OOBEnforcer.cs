using UnityEngine;
using System.Collections;

public class OOBEnforcer : MonoBehaviour {

	void OnTriggerStay2D(Collider2D coll) {
		Transform root = coll.transform.root.transform;
		root.position += -root.transform.position.normalized;

	}
}