using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    public bool devMode = false;

    public GameObject[] players;

	void Awake () {
	    if (instance == null) {
            instance = this;
        }
        if (instance != this) {
            Destroy(gameObject);
        }
        else {
            DontDestroyOnLoad(gameObject);
        }
	}

    void Start() {
        for (int i = 0; i < players.Length; ++i) {
            // TODO let players pick their number
            // for now controller number = player number
            Controller.controlMapping.Add(i, i);
        }
    }
	
	void Update () {
	    if (Input.GetKeyDown(KeyCode.F1)) {
            devMode = !devMode;
        }
	}
}
