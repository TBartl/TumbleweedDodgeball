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
		animator.SetFloat("moveSpeed", movement.GetSpeed());
	}


}
