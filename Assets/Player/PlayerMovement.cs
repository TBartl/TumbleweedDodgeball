using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour {
	PlayerColorizer colorizer;


	public float maxSpeed;
    public float dashSpeed;
    public float minCoolDownDash;
	Rigidbody2D rb;
	Controller controller;
    private bool isDashing = false;
	private bool canDash = true;

	public List<float> modifiers;

	void Awake()
	{
		rb = this.GetComponent<Rigidbody2D>();
		controller = this.GetComponent<Controller>();
		colorizer = this.GetComponent<PlayerColorizer>();
	}

	void FixedUpdate () {
		Vector3 direction = controller.GetMovementDirection();

        if(!isDashing) {
            float speed = maxSpeed;
            foreach (float f in modifiers)
                speed *= f;
            rb.velocity = direction * speed;
        }
        if (controller.GetDash() && Mathf.Abs(rb.velocity.magnitude) > 0.05f && canDash)
			StartCoroutine(Dash());
	}

    IEnumerator Dash() {
        isDashing = true;
		Vector2 direction = rb.velocity.normalized;
        for(float i = 0; i <= 0.5f / 3f; i += Time.deltaTime) {
            rb.velocity = direction * dashSpeed;
            yield return null;
        }
        isDashing = false;

		canDash = false;
		yield return new WaitForSeconds(minCoolDownDash);
		canDash = true;

		colorizer.FlashColor(Color.blue + Color.white * .7f);

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

	public float GetSpeed() {
		return rb.velocity.magnitude;
	}

	public float GetRotation() {
		Vector3 directionDiff = rb.velocity.normalized;
		return -90 + Mathf.Atan2(directionDiff.y, directionDiff.x) * Mathf.Rad2Deg;
	}

	public bool GetDashing() {
		return isDashing;
	}
}
