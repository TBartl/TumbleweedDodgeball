using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour {

	public float maxSpeed;
	Rigidbody2D rb;

	public List<float> modifiers;

	void Awake()
	{
		rb = this.GetComponent<Rigidbody2D>();
	}

	void FixedUpdate () {
		//TODO Input change to left analog stick
		Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0).normalized;

		float speed = maxSpeed;
		foreach (float f in modifiers)
			speed *= f;
		rb.velocity = direction * speed;
	}


}
