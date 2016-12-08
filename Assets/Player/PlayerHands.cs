using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerHands : MonoBehaviour {

    public List<Transform> hands;
    public List<Transform> throwPosition;

    public List<Ball> balls;
    bool doingSomething = false;
	List<bool> isChargingBall;

	PlayerData playerData;
    Controller controller;
	PlayerColorizer colorizer;

    public float throwPower = 20f;
    public Vector2 powerRange;
    public float superThrowPower;
    public float superThrowThreshold;
    public float chargeSpeed;
    public float rethrowDelay;

    public GameObject barPrefab;
    Transform resizableBar;

    public float aimAssistAngle;

    public MeshRenderer smackEffect;
    public float smackActive;
    public float smackWindDown;

	List<bool> isPunching;

	bool canSmack = true;
	public float smackCoolDown = 3f;

	PlayerHitBoxIncrease hitBoxIncrease;

	void Awake() {
        playerData = GetComponentInParent<PlayerData>();
		controller = GetComponentInParent<Controller>();
		colorizer = GetComponentInParent<PlayerColorizer>();
		smackEffect.enabled = false;
		hitBoxIncrease = transform.parent.GetComponentInChildren<PlayerHitBoxIncrease>();
    }

    void Start() {
		isChargingBall = new List<bool>(0);
		isChargingBall.Add(false);
		isChargingBall.Add(false);

		isPunching = new List<bool>(0);
		isPunching.Add(false);
		isPunching.Add(false);

		balls = new List<Ball>();
        for (int i = 0; i < hands.Count; i++) {
            balls.Add(null);
        }

        resizableBar = ((GameObject)Instantiate(barPrefab, this.transform.position, Quaternion.identity, this.transform)).transform.FindChild("BackgroundBar/Scalable");
        resizableBar.parent.parent.gameObject.SetActive(false);
    }

    void Update() {
        if (!doingSomething) {
            //For both hands
            for (int i = 0; i < 2; i++) {
                if (controller.GetHandActionDown(i)) {
                    if (balls[i] != null) {
                        StartCoroutine(ChargeThrowBall());
                    }
                    else if (canSmack)
                        StartCoroutine(Smack(i));
                    break;
                }
            }
        }
    }

    void LateUpdate() {
        for (int i = 0; i < balls.Count; i++) {
            if (balls[i] != null)
                balls[i].transform.position = hands[i].transform.position;
        }
    }

    IEnumerator AddBall(Ball b, int hand) {

        doingSomething = true;
        b.Grab(playerData.num);
        Vector3 originalPos = b.transform.position;
        Vector3 targetPos = hands[hand].position;

        float grabTime = .1f;
        for (float f = 0; f < grabTime; f += Time.deltaTime) {
            targetPos = hands[hand].position;
            b.transform.position = Vector3.Lerp(originalPos, targetPos, f / grabTime);
            yield return null;
        }

        b.transform.position = hands[hand].position;

        balls[hand] = b;
        doingSomething = false;
    }

    IEnumerator ChargeThrowBall() {
		hitBoxIncrease.Enable();

		doingSomething = true;
        //Get variables for this player's available powerups 
        bool maxCharge = PowerupManager.S.getPowerup(playerData.num) == Powerup.MaxCharge;
        resizableBar.parent.parent.gameObject.SetActive(true);
        transform.parent.GetComponent<PlayerMovement>().chargeBallSpeed = 0.2f; 

        float val = 0;

		// Buffers used to make sure player throws both balls when they intend to
		// otherwise it would require frame perfect precision
		float buffer0 = 0;
		float buffer1 = 0;

		while (true) {
			bool hand0Primed = (controller.GetHandActionHeld(0) && balls[0] != null);
			bool hand1Primed = (controller.GetHandActionHeld(1) && balls[1] != null);

			if (!hand0Primed && !hand1Primed)
				break;

			buffer0 -= Time.deltaTime;
			buffer1 -= Time.deltaTime;

			if (hand0Primed) {
				isChargingBall[0] = true;
				buffer0 = .15f;
			}
			else if (buffer0 <= 0)
				isChargingBall[0] = false;

			if (hand1Primed) {
				isChargingBall[1] = true;
				buffer1 = .15f;
			} else if (buffer1 <= 0)
				isChargingBall[1] = false;


			if (maxCharge) {
				val = 1;
				//break;
			}

			val += chargeSpeed * Time.deltaTime;
            if (val > 1) {
                val = 1;

				if (!DebugManager.noChargeDecrease)
					chargeSpeed *= -1;
            }
            if (val < 0) {
                val = 0;
                chargeSpeed *= -1;
            }
            controller.Vibrate(val / 2f);
            if (val >= 1 - superThrowThreshold)
                controller.Vibrate(1);
            resizableBar.transform.localScale = new Vector3(val, 1, 1);
            yield return null;
        }

        controller.Vibrate(0);

		if (isChargingBall[0] && balls[0]) {
			balls[0].transform.position = throwPosition[0].position;
			if (isChargingBall[1] && balls[1])
				balls[0].transform.position = throwPosition[2].position;
			balls[0].transform.rotation = Quaternion.identity;
		}
		if (isChargingBall[1] && balls[1]) {
			balls[1].transform.position = throwPosition[0].position;
			if (isChargingBall[0] && balls[0])
				balls[1].transform.position = throwPosition[1].position;
			balls[1].transform.rotation = Quaternion.identity;
		}
		
		resizableBar.transform.localScale = new Vector3(val, 1, 1);

		float power = Mathf.Lerp(powerRange.x, powerRange.y, val);
        if (val > 1 - superThrowThreshold && (!DebugManager.noChargeDecrease || val != 1)) {
            power = superThrowPower;
            resizableBar.transform.localScale = new Vector3(1, 2f, 2f);
        }

        Vector2 directionDiff = controller.GetDirection().normalized;
        directionDiff = AimAssist(directionDiff);
        
        for(int i = 0; i < 2; ++i) {
			if (isChargingBall[i]) {
				balls[i].Throw(directionDiff.normalized * power);
				balls[i].GetComponent<BallSource>().SetThrower(playerData);
				balls[i] = null;
			}
        }

		hitBoxIncrease.Disable();

        for (float t = 0; t < rethrowDelay; t += Time.deltaTime)
            yield return null;

		isChargingBall[0] = false;
		isChargingBall[1] = false;

        transform.parent.GetComponent<PlayerMovement>().chargeBallSpeed = 1f;
        resizableBar.parent.parent.gameObject.SetActive(false);
        doingSomething = false;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Ball" || other.tag == "Powerup") {
            other.GetComponent<BallGlow>().AddInRange(transform.parent.gameObject);
        }
    }

    IEnumerator Smack(int hand) {
        //Wait a frame so if we pick up a ball after Update() it's not a problem
        yield return new WaitForFixedUpdate();
        if (doingSomething)
            yield break;

        doingSomething = true;
		isPunching[hand] = true;
		canSmack = false;

        smackEffect.enabled = true;
        smackEffect.gameObject.layer = LayerMask.NameToLayer("Default");

		AudioManager.instance.PlayClipAtPoint(AudioManager.instance.punch, transform.position);

        for (float t = 0; t < smackActive; t += Time.deltaTime) {
            smackEffect.transform.localPosition = Vector3.Lerp(
                Vector3.forward * .1f,
                Vector3.zero,
                Mathf.Sin((t / smackActive) * Mathf.PI));
            yield return null;
        }
        smackEffect.gameObject.layer = LayerMask.NameToLayer("NoCollision");
        smackEffect.enabled = false;

		isPunching[hand] = false;

		yield return new WaitForSeconds(smackWindDown);

        doingSomething = false;
		
		yield return new WaitForSeconds(smackCoolDown);
		canSmack = true;
		colorizer.FlashColor(Color.yellow * .5f);
	}

    void OnTriggerStay2D(Collider2D other) {
        if (other.tag == "Ball") {
            if (!doingSomething) {
                Ball b = other.GetComponent<Ball>();

                bool leftTriggerHeld = controller.GetHandActionHeld(0);
                bool leftTriggerDown = controller.GetHandActionDown(0);
                bool rightTriggerHeld = controller.GetHandActionHeld(1);
                bool rightTriggerDown = controller.GetHandActionDown(1);
                bool ballIsHot = b.hotness.GetIsHot();

                if (balls[0] == null && ((ballIsHot && leftTriggerDown && b.catchable) || (!ballIsHot && leftTriggerHeld))) {
                    StartCoroutine(AddBall(b, 0));
                }
                else if (balls[1] == null && ((ballIsHot && rightTriggerDown && b.catchable) || (!ballIsHot && rightTriggerHeld))) {
                    StartCoroutine(AddBall(b, 1));
                }

            }
        }
        else if (other.tag == "Powerup") {
            bool leftTriggerHeld = controller.GetHandActionHeld(0);
            bool leftTriggerDown = controller.GetHandActionDown(0);
            bool rightTriggerDown = controller.GetHandActionDown(1);
            bool rightTriggerHeld = controller.GetHandActionHeld(1);
            if (balls[0] == null && (leftTriggerDown || leftTriggerHeld)) other.GetComponent<PowerupController>().PickUp(playerData.num);
            else if (balls[1] == null && (rightTriggerDown || rightTriggerHeld)) other.GetComponent<PowerupController>().PickUp(playerData.num);
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Ball" || other.tag == "Powerup") {
            other.GetComponent<BallGlow>().RemoveInRange(transform.parent.gameObject);
        }
    }

    Vector2 AimAssist(Vector2 currentDirection) {
        float angle, smallestAngle = float.MaxValue;
        Vector2 newDirection = currentDirection;
        foreach (GameObject player in PlayerManager.inst.players) {
            if (player.GetComponent<PlayerData>().num != playerData.num) {
                Vector2 playerDiff = (player.transform.position - this.transform.position).normalized;
                angle = Vector2.Angle(currentDirection, playerDiff);
                if (angle < smallestAngle) {
                    smallestAngle = angle;
                    newDirection = playerDiff;
                }
            }
        }

        if (smallestAngle <= aimAssistAngle) {
            return newDirection;
        }
        return currentDirection;
    }

	public bool GetHandUp(int hand) {
		return (balls[hand] != null);
	}

	public bool GetIsChargingBall(int hand) {
		return isChargingBall[hand];
	}

	public bool GetIsPunching(int hand) {
		return isPunching[hand];
	}
}
