using UnityEngine;
using System.Collections;

public class BallSource : MonoBehaviour {

	PlayerData thrower;

	public void SetThrower(PlayerData t) {
		thrower = t;
	}

	public PlayerData GetThrower() {
		return thrower;
	}

}
