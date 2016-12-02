using UnityEngine;
using System.Collections;

public class BallWind : MonoBehaviour {

	// Use this for initialization
	void Start () {
		WindManager.instance.AddBall(GetComponent<Rigidbody2D>());
	}

	void OnDestroy() {
		WindManager.instance.RemoveBall(GetComponent<Rigidbody2D>());
	}

}
