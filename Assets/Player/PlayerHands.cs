using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerHands : MonoBehaviour {

	public List<Transform> hands;
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

	void Awake() {
		playerData = GetComponentInParent<PlayerData>();
		controller = GetComponentInParent<Controller>();
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
			if (balls[0] != null && controller.GetHandActionDown(0))
				StartCoroutine(ChargeThrowBall(0));
			else if (balls[1] != null && controller.GetHandActionDown(1))
				StartCoroutine(ChargeThrowBall(1));
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

	IEnumerator ChargeThrowBall(int hand) {
		doingSomething = true;
		resizableBar.parent.parent.gameObject.SetActive(true);
		transform.parent.GetComponent<PlayerMovement>().modifiers.Add(.2f);

		float val = 0;
		while (controller.GetHandActionHeld(hand)) {
			val += chargeSpeed * Time.deltaTime;
			if (val > 1) {
				val = 1;
				chargeSpeed *= -1;
			}
			if (val < 0) {
				val = 0;
				chargeSpeed *= -1;
			}
			resizableBar.transform.localScale = new Vector3(val, 1, 1);
			yield return null;
		}

		float power = Mathf.Lerp(powerRange.x, powerRange.y, val);
		if (val > 1 - superThrowThreshold) {
			power = superThrowPower;
			resizableBar.transform.localScale = new Vector3(1, 2f, 2f);
		}

		Vector2 directionDiff = controller.GetDirection();
		balls[hand].Throw(directionDiff.normalized * power);
		balls[hand].GetComponent<BallSource>().SetSourceID(playerData.num);
		balls[hand] = null;

		for (float t = 0; t < rethrowDelay; t += Time.deltaTime)
			yield return null;


		transform.parent.GetComponent<PlayerMovement>().modifiers.Remove(.2f);
		resizableBar.parent.parent.gameObject.SetActive(false);
		doingSomething = false;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Ball") {
			other.GetComponent<BallGlow>().AddInRange(transform.parent.gameObject);
		}
	}

	void OnTriggerStay2D(Collider2D other) {
		if (other.tag == "Ball") {
			if (!doingSomething) {
				Ball b = other.GetComponent<Ball>();

				// TODO add trigger stuff here too
				bool leftTriggerHeld = controller.GetHandActionHeld(0);
				bool leftTriggerDown = controller.GetHandActionDown(0);
				bool rightTriggerHeld = controller.GetHandActionHeld(1);
				bool rightTriggerDown = controller.GetHandActionDown(1);
				bool ballIsHot = b.hotness.GetIsHot();
				if (balls[0] == null && (leftTriggerDown || (leftTriggerHeld && !ballIsHot)))
					StartCoroutine(AddBall(b, 0));
				else if (balls[1] == null && (rightTriggerDown || (rightTriggerHeld && !ballIsHot)))
					StartCoroutine(AddBall(b, 1));
			}
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "Ball") {
			other.GetComponent<BallGlow>().RemoveInRange(transform.parent.gameObject);
		}
	}
}
