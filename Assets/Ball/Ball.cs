using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {
	protected Rigidbody2D rb;
	Collider2D col;
	BounceVertical bounce;
	BallGlow glow;
	BallSource source;
	[HideInInspector]
	public BallHotness hotness;

	void Awake() {
		rb = this.GetComponent<Rigidbody2D>();
		col = this.GetComponent<Collider2D>();
		bounce = this.GetComponentInChildren<BounceVertical>();
		glow = this.GetComponent<BallGlow>();
		hotness = this.GetComponent<BallHotness>();
		source = this.GetComponent<BallSource>();
	}

	void Update () {
		if (!hotness.GetIsHot() && source.throwID != -1 && !source.held) source.setID(-1);
	}

	public void Grab(int ID) {	
		col.enabled = false;
		rb.isKinematic = true;
		glow.Grabbed();
		source.setID(ID);
		source.holdBall();
	}

	public virtual void Throw (Vector3 velocity) {
		col.enabled = true;
		rb.isKinematic = false;
		rb.velocity = velocity;
		transform.parent = null;
		bounce.Reset();
		source.releaseBall();
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Player" && source.throwID != -1 && other.gameObject.GetComponent<PlayerData>().playerNum != source.throwID) {
			print(source.throwID);
			PlayerAddHit.AddHit(source.throwID);
		}
	}
}
