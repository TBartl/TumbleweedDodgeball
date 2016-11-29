using UnityEngine;
using System.Collections;

public enum Powerup {
    IncreasedSpeed,
    IncreasedGrabRange,
    QuickCharge,
    MaxCharge,
    Invincible,
    Targeting,
    NoPowerup
}

public class PowerupManager : MonoBehaviour {

    static public PowerupManager S;
    Powerup[] playerPowerup = new Powerup[4];

    void Awake() {
        S = this;
        for (int i = 0; i < 4; ++i) playerPowerup[i] = Powerup.NoPowerup; //init array
    }

    bool isPlayerNumValid(int playerNum) {
        if (playerNum >= 4 || playerNum < 0) { //check for validity
            Debug.Log("player num is incorrect");
            return false;
        }
        return true;
    }

    public bool hasPowerup(int player) {
        return playerPowerup[player] != Powerup.NoPowerup;
    }

    public void setPowerup(int player, Powerup newPower) {
        if (!isPlayerNumValid(player)) return;
        playerPowerup[player] = newPower;
    }
    
    public Powerup getPowerup(int player) {
        if (!isPlayerNumValid(player)) return Powerup.NoPowerup;
        return playerPowerup[player];
    }
}
