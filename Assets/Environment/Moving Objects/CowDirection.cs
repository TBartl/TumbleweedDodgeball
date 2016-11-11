using UnityEngine;
using System.Collections;

public class CowDirection : MonoBehaviour {

	Vector3 prevPosition;
	CowMovement movement;

	// Use this for initialization
	void Start () {
		prevPosition = transform.position;
		movement = GetComponent<CowMovement>();
	}

	// Update is called once per frame
	void Update () {
		if (movement.currentSpeed != 0 && transform.position != prevPosition) {
			transform.rotation = Quaternion.LookRotation(transform.position - prevPosition);
			prevPosition = transform.position;
		}
	}
}
