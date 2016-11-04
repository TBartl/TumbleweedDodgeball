using UnityEngine;
using System.Collections;


public class PlayerDirection : MonoBehaviour {

    PlayerData playerData;

    void Start() {
        playerData = GetComponentInParent<PlayerData>();
    }

	void Update () {
        Vector3 directionDiff = Controller.GetDirection(playerData.playerNum);
		transform.rotation = Quaternion.Euler(0, 0, -90 + Mathf.Atan2(directionDiff.y, directionDiff.x) * Mathf.Rad2Deg);
	}
}
