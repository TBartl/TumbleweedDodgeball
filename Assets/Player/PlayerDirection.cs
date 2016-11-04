using UnityEngine;
using System.Collections;


public class PlayerDirection : MonoBehaviour {

    PlayerData playerData;
	Controller controller;

    void Start() {
        playerData = GetComponentInParent<PlayerData>();
		controller = GetComponentInParent<Controller>();
    }

	void Update () {
		Vector3 directionDiff = controller.GetDirection();
		transform.rotation = Quaternion.Euler(0, 0, -90 + Mathf.Atan2(directionDiff.y, directionDiff.x) * Mathf.Rad2Deg);
	}
}
