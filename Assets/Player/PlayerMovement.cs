using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour {

	public float maxSpeed;
	Rigidbody2D rb;
	Controller controller;

	public List<float> modifiers;

	void Awake()
	{
		rb = this.GetComponent<Rigidbody2D>();
		controller = this.GetComponent<Controller>();
	}

	void FixedUpdate () {
		Vector3 direction = controller.GetMovementDirection();

		float speed = maxSpeed;
		foreach (float f in modifiers)
			speed *= f;
		rb.velocity = direction * speed;
	}


}
