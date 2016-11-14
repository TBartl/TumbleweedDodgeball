using UnityEngine;
using System.Collections;

public class PowerupController : MonoBehaviour {

    public Powerup powerup;

    public void PickUp(int playerNum) {
        PowerupManager.S.setPowerup(playerNum, powerup);
        Destroy(this.gameObject);
    }
}
