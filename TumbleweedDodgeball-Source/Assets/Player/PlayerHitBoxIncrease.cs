using UnityEngine;
using System.Collections;

public class PlayerHitBoxIncrease : MonoBehaviour {

	CircleCollider2D coll;

	// Use this for initialization
	void Start () {
		coll = GetComponent<CircleCollider2D>();
	}

	public void Enable() {
		coll.enabled = true;
	}

	public void Disable() {
		coll.enabled = false;
	}
}
