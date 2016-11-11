using UnityEngine;
using System.Collections;

public class CowMovement : MonoBehaviour {

	private enum State {STOPPED, ACCELERATING, STEADY, DECCELERATING}

	public float maxSpeed, acceleration, maxDuration;
	public float currentSpeed;
	float changeStateTime;
	State state = State.STOPPED;
	
	//float speed, changeSpeedTime;
	//public float maxSpeed, maxDuration;
	Vector3[] path;
	float percent = 0;

	// Use this for initialization
	void Start () {
		changeStateTime = Time.time + Random.Range(0f, maxDuration);
		path = iTweenPath.GetPath("Cow Path");
	}
	
	// Update is called once per frame
	void Update () {
		switch (state) {
			case State.STOPPED:
				if (Time.time >= changeStateTime) {
					ChangeState();
				}
				currentSpeed = 0;
				break;
			case State.ACCELERATING:
				if (currentSpeed >= maxSpeed) {
					ChangeState();
					break;
				}
				Accelerate();
				break;
			case State.STEADY:
				if (Time.time >= changeStateTime) {
					ChangeState();
				}
				currentSpeed = maxSpeed;
				break;
			case State.DECCELERATING:
				if (currentSpeed <= 0) {
					ChangeState();
					break;
				}
				Deccelerate();
				break;
		}

		percent += currentSpeed * Time.deltaTime;
		if (percent > 1) {
			percent -= 1;
		}

		iTween.PutOnPath(gameObject, path, percent);
	}

	void ChangeState() {
		switch (state) {
			case State.STOPPED:
				state = State.ACCELERATING;
				break;
			case State.ACCELERATING:
				state = State.STEADY;
				changeStateTime = Time.time + Random.Range(0f, maxDuration);
				break;
			case State.STEADY:
				state = State.DECCELERATING;
				break;
			case State.DECCELERATING:
				state = State.STOPPED;
				changeStateTime = Time.time + Random.Range(0f, maxDuration);
				break;
		}
	}

	void Accelerate() {
		currentSpeed += acceleration * Time.deltaTime;
	}

	void Deccelerate() {
		currentSpeed -= acceleration * Time.deltaTime;
	}


}
