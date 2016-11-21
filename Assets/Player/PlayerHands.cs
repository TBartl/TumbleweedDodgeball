using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerHands : MonoBehaviour {

	public List<Transform> hands;
	public Transform throwPosition;

	List<Ball> balls;
	bool doingSomething = false;

    PlayerData playerData;
	Controller controller;

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
	public float smackCooldown;

	void Awake() {
		playerData = GetComponentInParent<PlayerData>();
		controller = GetComponentInParent<Controller>();
		smackEffect.enabled = false;
	}

	void Start() {

		//TODO replace hands with predetermined bones in the armature
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
					if (balls[i] != null)
						StartCoroutine(ChargeThrowBall(i));
					else
						StartCoroutine(Smack());
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

		try {
			if (hand == 0) TutorialManager.lHands[playerData.num] = true;
			if (hand == 1) TutorialManager.rHands[playerData.num] = true;
		} catch {
			//do nothing
		}

		b.Grab(playerData.num);
		AddBallUI.BalltoUI(playerData.num, hand);
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

	IEnumerator ChargeThrowBall(int hand) {
		doingSomething = true;
        //Get variables for this player's available powerups 
        bool instantThrow = PowerupManager.S.getPowerup(controller.inputDeviceNum) == Powerup.ThrowQuick;
        float increasedCharge = PowerupManager.S.getPowerup(controller.inputDeviceNum) == Powerup.QuickCharge ? 2 : 1;
        resizableBar.parent.parent.gameObject.SetActive(true);
		transform.parent.GetComponent<PlayerMovement>().modifiers.Add(.2f);

        float val = 0;
		
		if (!instantThrow) {
            while (controller.GetHandActionHeld(hand)) {
                val += chargeSpeed * Time.deltaTime * increasedCharge;
                if (val > 1) {
                    val = 1;
                    chargeSpeed *= -1;
                }
                if (val < 0) {
                    val = 0;
                    chargeSpeed *= -1;
                }
                controller.Vibrate(val);
                resizableBar.transform.localScale = new Vector3(val, 1, 1);
                yield return null;
            }
        }

		balls[hand].transform.position = throwPosition.position;


		float power = instantThrow ? throwPower : Mathf.Lerp(powerRange.x, powerRange.y, val);
		if (val > 1 - superThrowThreshold) {
			power = superThrowPower;
			resizableBar.transform.localScale = new Vector3(1, 2f, 2f);
		}

		Vector2 directionDiff = controller.GetDirection().normalized;
		directionDiff = AimAssist(directionDiff);
		balls[hand].Throw(directionDiff.normalized * power);
		balls[hand].GetComponent<BallSource>().SetThrower(playerData);
		balls[hand] = null;
		AddBallUI.BallfromUI(playerData.num, hand);

		for (float t = 0; t < rethrowDelay; t += Time.deltaTime)
			yield return null;


		transform.parent.GetComponent<PlayerMovement>().modifiers.Remove(.2f);
		resizableBar.parent.parent.gameObject.SetActive(false);
		doingSomething = false;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Ball" || other.tag == "Powerup") {
			other.GetComponent<BallGlow>().AddInRange(transform.parent.gameObject);
		}
	}

	IEnumerator Smack() {
		//Wait a frame so if we pick up a ball after Update() it's not a problem
		yield return new WaitForFixedUpdate();
		if (doingSomething)
			yield break;

		doingSomething = true;

		smackEffect.enabled = true;
		smackEffect.gameObject.layer = LayerMask.NameToLayer("Default");
		for (float t = 0; t < smackActive; t += Time.deltaTime) {
			smackEffect.transform.localPosition = Vector3.Lerp(
				Vector3.forward * .1f,
				Vector3.zero,
				Mathf.Sin((t/smackActive)*Mathf.PI));
			yield return null;
		}
		smackEffect.gameObject.layer = LayerMask.NameToLayer("NoCollision");
		smackEffect.enabled = false;

		yield return new WaitForSeconds(smackCooldown);

		doingSomething = false;
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
        else if(other.tag == "Powerup") {
            bool leftTriggerHeld = controller.GetHandActionHeld(0);
            bool leftTriggerDown = controller.GetHandActionDown(0);
            bool rightTriggerDown = controller.GetHandActionDown(1);
            bool rightTriggerHeld = controller.GetHandActionHeld(1);
            if (balls[0] == null && (leftTriggerDown ||leftTriggerHeld)) other.GetComponent<PowerupController>().PickUp(playerData.num);
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
				//Debug.DrawRay(transform.position + Vector3.back, playerDiff, Color.green, 3f);
				//Debug.DrawRay(transform.position + Vector3.back, currentDirection, Color.blue, 3f);
				angle = Vector2.Angle(currentDirection, playerDiff);
				if (angle < smallestAngle) {
					smallestAngle = angle;
					newDirection = playerDiff;
				}
			}
		}

		if (smallestAngle <= aimAssistAngle) {
			//Debug.DrawRay(transform.position + Vector3.back, newDirection, Color.red, 3f);
			return newDirection;
		}
		return currentDirection;
	}
}