using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {
	protected Rigidbody2D rb;
	Collider2D col;
	BounceVertical bounce;
	BallGlow glow;
	[HideInInspector]
	public BallHotness hotness;
	public bool catchable;

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
		glow.Throwed();
		col.enabled = true;
		rb.isKinematic = false;
		rb.velocity = velocity;
		transform.parent = null;
		bounce.Reset();
		hotness.makeHot();

		AudioManager.instance.PlayClipAtPoint(AudioManager.instance.throwSound, transform.position);
	}
}
