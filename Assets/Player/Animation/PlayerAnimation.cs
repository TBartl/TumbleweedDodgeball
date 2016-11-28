using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour {
	PlayerMovement movement;
	PlayerDirection direction;
	Animator animator;
	PlayerHands hands;
	
	void Awake() {
		movement = GetComponent<PlayerMovement>();
		direction = GetComponentInChildren<PlayerDirection>();
		animator = this.GetComponentInChildren<Animator>();
		hands = this.GetComponentInChildren<PlayerHands>();
	}

	void Update() {
		float speed = movement.GetSpeed();

		animator.SetBool("moving", speed > .05f);

		float angleDiff = -movement.GetRotation() + direction.GetRotation();
		if (Mathf.Abs(angleDiff) > 90) {
			speed = -speed;
			if (angleDiff < 0)
				angleDiff += 180;
			else if (angleDiff > 0)
				angleDiff -= 180;
		}

		animator.SetFloat("moveSpeed", speed);
		animator.SetFloat("rotation", angleDiff);

		animator.SetBool("leftHandUp", hands.GetHandUp(0));
		animator.SetBool("rightHandUp", hands.GetHandUp(1));

		animator.SetBool("leftHandCharge", hands.GetIsChargingBall(0));
		animator.SetBool("rightHandCharge", hands.GetIsChargingBall(1));

		animator.SetBool("leftHandPunch", hands.GetIsPunching(0));
		animator.SetBool("rightHandPunch", hands.GetIsPunching(1));
	}


}
