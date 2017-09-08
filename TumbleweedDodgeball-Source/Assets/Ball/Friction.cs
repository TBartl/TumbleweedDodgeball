using UnityEngine;
using System.Collections;

public class Friction : MonoBehaviour {
	public float force;
	Rigidbody2D rb;

	void Awake()
	{
		rb = this.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void LateUpdate () {
		float currentSpeed = rb.velocity.magnitude;
		rb.velocity = rb.velocity.normalized * Mathf.Max(0, currentSpeed - force * Time.deltaTime);	
	}
}
