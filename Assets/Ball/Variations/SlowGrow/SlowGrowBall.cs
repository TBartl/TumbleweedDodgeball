using UnityEngine;
using System.Collections;

public class SlowGrowBall : Ball {

    public float maxSize;
    private bool wasThrown = false;
    private float originalScale;

    void Start() {
        originalScale = this.transform.localScale.x;
    }

    public override void Throw(Vector3 velocity) {
        wasThrown = true;
        base.Throw(velocity);
    }

    void FixedUpdate() {
        if (!wasThrown) return;
        else if (wasThrown && base.rb.velocity.x == 0) {
            Reset();
            return;
        }
        float currentScale = this.transform.localScale.x;
        if (currentScale >= maxSize) return;
        float newScale = currentScale + 0.02f;
        this.transform.localScale = new Vector3(newScale, newScale, newScale);
    }

    void OnCollisionEnter2D(Collision2D col) {
       if(col.gameObject.tag == "Player" && (col.gameObject.GetComponent<PlayerData>().num != GetComponent<BallSource>().GetThrower().num)) Destroy(this.gameObject);
       else if(col.gameObject.tag == "Wall" ||col.gameObject.tag == "GameController") {
            Reset();
        }
    }

    void Reset() {
        wasThrown = false;
        this.transform.localScale = new Vector3(originalScale, originalScale, originalScale);
    }
}
