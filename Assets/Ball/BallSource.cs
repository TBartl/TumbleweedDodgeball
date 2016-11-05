using UnityEngine;
using System.Collections;

public class BallSource : MonoBehaviour {

	public int throwID;
	public bool held;

	void Awake () {
		throwID = -1;
		held = false;
	}
	
	public void setID (int ID) {
		throwID = ID;
	}

	public void holdBall () {
		held = true;
	}

	public void releaseBall () {
		held = false;
	}
}
