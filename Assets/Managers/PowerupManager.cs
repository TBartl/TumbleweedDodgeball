using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
    Dictionary<Powerup, int> timeOutSeconds;
    Powerup[] playerPowerup = new Powerup[4];

    void Awake() {
        S = this;
        for (int i = 0; i < 4; ++i) playerPowerup[i] = Powerup.NoPowerup; //init array

        //init dictionary
        timeOutSeconds = new Dictionary<Powerup, int>();
        timeOutSeconds.Add(Powerup.IncreasedSpeed, 10);
        timeOutSeconds.Add(Powerup.IncreasedGrabRange, 10);
        timeOutSeconds.Add(Powerup.QuickCharge, 10);
        timeOutSeconds.Add(Powerup.MaxCharge, 10);
        timeOutSeconds.Add(Powerup.Invincible, 10);
        timeOutSeconds.Add(Powerup.Targeting, 10);
    }

    bool isPlayerNumValid(int playerNum) {
        if (playerNum >= 4 || playerNum < 0) { //check for validity
            Debug.Log("player num is incorrect");
            return false;
        }
        return true;
    }

    int getTimeOut(Powerup power) {
        int timeout;
        timeOutSeconds.TryGetValue(power, out timeout);
        return timeout;
    }

    public bool hasPowerup(int player) {
        return playerPowerup[player] != Powerup.NoPowerup;
    }

    public void setPowerup(int player, Powerup newPower) {
        if (!isPlayerNumValid(player)) return;
        playerPowerup[player] = newPower;
        StartCoroutine(TimeOutPowerup(newPower, getTimeOut(newPower), player)); //reset powerup after num seconds
    }
    
    public Powerup getPowerup(int player) {
        if (!isPlayerNumValid(player)) return Powerup.NoPowerup;
        return playerPowerup[player];
    }

    IEnumerator TimeOutPowerup(Powerup power, int timeOutSeconds, int playerID) {
        for(float i = 0; i < timeOutSeconds; i += Time.deltaTime) {
            if (playerPowerup[playerID] != power) yield break;
            yield return null;
        }
        if (playerPowerup[playerID] == power) playerPowerup[playerID] = Powerup.NoPowerup; //reset 
    } 
}
