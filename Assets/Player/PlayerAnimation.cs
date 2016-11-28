using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour {
	PlayerMovement movement;
	PlayerDirection direction;
	Animator animator;
	
	void Awake() {
		movement = GetComponent<PlayerMovement>();
		direction = GetComponentInChildren<PlayerDirection>();
		animator = this.GetComponentInChildren<Animator>();
	}

	void Update() {
		float speed = movement.GetSpeed();

		animator.SetBool("moving", speed > .05f);

		float angleDiff = -movement.GetRotation() + direction.GetRotation();
		if (Mathf.Abs(angleDiff) > 90) {
			speed = -speed;
			if (angleDiff < 0)
				angleDiff += 180;
			if (angleDiff < 0)
				angleDiff -= 180;
		}
		Debug.Log(angleDiff);

		animator.SetFloat("moveSpeed", speed);
		animator.SetFloat("rotation", angleDiff);
	}


}
