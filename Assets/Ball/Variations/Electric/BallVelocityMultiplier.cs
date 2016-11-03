using UnityEngine;
using System.Collections;

public class BallVelocityMultiplier : Ball {
	public float multiplier = 3f;

	public override void Throw (Vector3 velocity)
	{
		base.Throw(velocity);
		rb.velocity = velocity * multiplier;
	}
}
