using UnityEngine;
using System.Collections;

public class BallSource : MonoBehaviour {

	int sourceID;

	void Awake () {
		sourceID = -1;
	}

	public void  SetSourceID(int newSourceID) {
		sourceID = newSourceID;
	}

	public int GetSourceID() {
		return sourceID;
	}

}
