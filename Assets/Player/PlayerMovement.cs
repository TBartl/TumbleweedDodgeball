using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour {
	PlayerColorizer colorizer;
	PlayerData data;

	public float maxSpeed;
    public float dashSpeed;
    public float minCoolDownDash;
	Rigidbody2D rb;
	Controller controller;
    private bool isDashing = false;
	private bool canDash = true;

	public List<float> modifiers;
    public float dazeMovement = 1f;
    public float chargeBallSpeed = 1f;
	public float powerupMoveIncrease = 2f;

	void Awake()
	{
		modifiers = new List<float>();
		rb = this.GetComponent<Rigidbody2D>();
		controller = this.GetComponent<Controller>();
		colorizer = this.GetComponent<PlayerColorizer>();
		data = this.GetComponent<PlayerData>();
	}

	void FixedUpdate () {
		Vector3 direction = controller.GetMovementDirection();

        if(!isDashing) {
            float speed = maxSpeed;
            /*foreach (float f in modifiers)
                speed *= f;*/
            speed *= dazeMovement;
            speed *= chargeBallSpeed;
			if (PowerupManager.S.getPowerup(data.num) == Powerup.IncreaseMoveSpeed) {
				speed *= powerupMoveIncrease;
			}

            rb.velocity = direction * speed;
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

		colorizer.FlashColor(Color.blue * .5f);

    }

    public IEnumerator KnockBack(Vector2 velocityHit) {
        for(float i = 0; i <= .2f; i += Time.deltaTime) {
			this.transform.position += (Vector3)velocityHit * Time.deltaTime * .2f;
            yield return null;
        }
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
