using UnityEngine;
using System.Collections;

public class GhostBall : MonoBehaviour {

    private Ball ball;
    private Rigidbody2D rb;
    private int GhostBallLayer;
    private int DefaultLayer;

    void Start() {
        ball = GetComponent<Ball>(); //parent Ball script
        rb = GetComponent<Rigidbody2D>();//parent rigidbody2D
        GhostBallLayer = LayerMask.NameToLayer("Ghostball");
        DefaultLayer = LayerMask.NameToLayer("Default");
    }

    //if outside of view, destroy it
	void FixedUpdate() {

        if(Mathf.Abs(rb.velocity.x) > 0) { //ball was thrown
            if (ball.hotness.GetIsHot()) this.gameObject.layer = GhostBallLayer;
            else this.gameObject.layer = DefaultLayer;
        }

        Vector3 posOnScreen = Camera.main.WorldToViewportPoint(this.transform.position);
        bool isSeen = posOnScreen.x > 0 && posOnScreen.x < 1 && posOnScreen.y > 0 && posOnScreen.y < 1;
        if (!isSeen) Destroy(this.gameObject);
    }
}
