using UnityEngine;
using System.Collections;

public class BallSquish : MonoBehaviour {
	bool squishing = false, unsquishing = false, colliding = false;
	public float squishFactor;
	public float stretchFactor;
	public float squishDuration;
	float squishStartTime, squishFinishTime, unsquishStartTime, unsquishFinishTime;
	Vector2 squishVector, stretchVector, ballDir;
	
	Ball ball;
	Rigidbody2D body;

	void Start() {
		ball = GetComponent<Ball>();
		body = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if(!colliding) ballDir = body.velocity.normalized;
		if (squishing) {
			if (Time.time >= squishFinishTime) {
				squishing = false;
				unsquishing = true;
				unsquishStartTime = Time.time;
				unsquishFinishTime = unsquishStartTime + squishDuration;
			}
			else {
				Vector2 currentSquishVector = (squishVector * (Time.time - squishStartTime) / (squishFinishTime - squishStartTime));
				Vector2 currentStretchVector = (stretchVector * (Time.time - squishStartTime) / (squishFinishTime - squishStartTime));
				transform.localScale = new Vector3(transform.localScale.x - currentSquishVector.x + currentStretchVector.x, 
					transform.localScale.y - currentSquishVector.y + currentStretchVector.y, transform.localScale.z);
			}
		}
		if (unsquishing) {
			if (Time.time >= unsquishFinishTime) {
				unsquishing = false;
				transform.localScale = Vector3.one;
			}
			else {
				Vector2 currentSquishVector = (squishVector * (Time.time - unsquishStartTime) / (unsquishFinishTime - unsquishStartTime));
				Vector2 currentStretchVector = (stretchVector * (Time.time - unsquishStartTime) / (unsquishFinishTime - unsquishStartTime));
				transform.localScale = new Vector3(transform.localScale.x + currentSquishVector.x - currentStretchVector.x, 
					transform.localScale.y + currentSquishVector.y - currentStretchVector.y, transform.localScale.z);
			}
		}
		if (!squishing && !unsquishing && colliding) colliding = false;
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (ball.hotness.GetIsHot()) { // only squish if the ball is hot
			colliding = true;
			Vector2 direction = ballDir;//new Vector3(Mathf.Abs(body.velocity.x), Mathf.Abs(body.velocity.y));
			squishStartTime = Time.time;
			squishFinishTime = squishStartTime + squishDuration;
			squishing = true;
			squishVector = direction*squishFactor;
			direction = Quaternion.Euler(new Vector3(0, 0, 90)) * direction;
			stretchVector = direction/stretchFactor;
		}
	}
}
