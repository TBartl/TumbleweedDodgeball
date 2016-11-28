using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour {
	PlayerMovement movement;
	Animator animator;
	
	void Awake() {
		movement = GetComponent<PlayerMovement>();
		animator = this.GetComponentInChildren<Animator>();
	}

	void Update() {
		animator.SetFloat("moveSpeed", movement.GetSpeed());
	}


}
