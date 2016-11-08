using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {
	protected Rigidbody2D rb;
	Collider2D col;
	BounceVertical bounce;
	BallGlow glow;
	[HideInInspector]
	public BallHotness hotness;
	public float squishFactor;

	void Awake() {
		rb = this.GetComponent<Rigidbody2D>();
		col = this.GetComponent<Collider2D>();
		bounce = this.GetComponentInChildren<BounceVertical>();
		glow = this.GetComponent<BallGlow>();
		hotness = this.GetComponent<BallHotness>();
	}

	public void Grab(int ID) {	
		col.enabled = false;
		rb.isKinematic = true;
		glow.Grabbed();
	}

	public virtual void Throw (Vector3 velocity) {
		col.enabled = true;
		rb.isKinematic = false;
		rb.velocity = velocity;
		transform.parent = null;
		bounce.Reset();
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (hotness.GetIsHot()) { // only squish if the ball is hot
			Vector3 direction = rb.velocity.normalized;
			transform.localScale = new Vector3(transform.localScale.x - squishFactor * direction.x, transform.localScale.y - squishFactor * direction.y, transform.localScale.z);
			Invoke("Unsquish", 1);
		}
	}

	void Unsquish() {
		transform.localScale = Vector3.one;
	}
}
