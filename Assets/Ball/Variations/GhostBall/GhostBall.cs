using UnityEngine;
using System.Collections;

public class GhostBall : MonoBehaviour {

    //if outside of view, destroy it
	void FixedUpdate() {
        Vector3 posOnScreen = Camera.main.WorldToViewportPoint(this.transform.position);
        bool isSeen = posOnScreen.x > 0 && posOnScreen.x < 1 && posOnScreen.y > 0 && posOnScreen.y < 1;
        if (!isSeen) Destroy(this.gameObject);
    }
}
