using UnityEngine;
using System.Collections;


public class PlayerDirection : MonoBehaviour {
	Controller controller;

	void Awake() {
		controller = GetComponentInParent<Controller>();
	}

	void Update () {
		transform.rotation = Quaternion.Euler(0, 0, GetRotation());
	}

	public float GetRotation() {
		Vector3 directionDiff = controller.GetDirection();
		return -90 + Mathf.Atan2(directionDiff.y, directionDiff.x) * Mathf.Rad2Deg;
	}
}
