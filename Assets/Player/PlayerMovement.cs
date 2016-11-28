using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour {

	public float maxSpeed;
    public float dashSpeed;
    public float minCoolDownDash;
	Rigidbody2D rb;
	Controller controller;
    private bool isDashing = false;
    private float lastDash = 0;

	public List<float> modifiers;

	void Awake()
	{
		rb = this.GetComponent<Rigidbody2D>();
		controller = this.GetComponent<Controller>();
	}

	void FixedUpdate () {
		Vector3 direction = controller.GetMovementDirection();

        if(!isDashing) {
            float speed = maxSpeed;
            foreach (float f in modifiers)
                speed *= f;
            rb.velocity = direction * speed;
        }
        if (controller.GetDash() && Mathf.Abs(rb.velocity.x) > 0.5f && (Time.time - lastDash > minCoolDownDash)) StartCoroutine(Dash());
	}

    IEnumerator Dash() {
        isDashing = true;
        for(float i = 0; i <= 0.5f / 3f; i += Time.deltaTime) {
            rb.velocity = new Vector2(getXVelocitySpeed(), getYVelocitySpeed());
            yield return null;
        }
        isDashing = false;
        lastDash = Time.time;
    }

    public IEnumerator KnockBack(Vector2 velocityHit) {
        for(float i = 0; i <= 1f; i += Time.deltaTime) {
            rb.velocity = new Vector2(GenerateKnockBackSpeedX(velocityHit.x), 
                                        GenerateKnockBackSpeedY(velocityHit.y));
            yield return null;
        }
    }

    float GenerateKnockBackSpeedX(float xVel) {
        float knockBackX = 0f;
        if (xVel > 0) {
            if (xVel > 10f) knockBackX = 10f;
            else knockBackX = xVel;
        }
        else {
            if (xVel < -10f) knockBackX = -10f;
            else knockBackX = xVel;
        }
        return knockBackX;
    }

    float GenerateKnockBackSpeedY(float yVel) {
        float knockBackY = 0f;
        if (yVel > 0) {
            if (yVel > 10f) knockBackY = 10f;
            else knockBackY = yVel;
        }
        else {
            if (yVel < -10f) knockBackY = -10f;
            else knockBackY = yVel;
        }
        return knockBackY;
    }

    float getXVelocitySpeed() {
        if (rb.velocity.x > 0.3f) return dashSpeed;
        else if (rb.velocity.x < -0.3f) return -dashSpeed;
        else return 0;
    }

    float getYVelocitySpeed() {
        if (rb.velocity.y > 0.3f) return dashSpeed;
        else if (rb.velocity.y < -0.3f) return -dashSpeed;
        else return 0;
    }

	public float GetSpeed() {
		return rb.velocity.magnitude;
	}

	public bool GetDashing() {
		return isDashing;
	}
}
