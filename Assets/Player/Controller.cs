﻿using UnityEngine;
using System.Collections;
using InControl;

public class Controller : MonoBehaviour {

	public int inputDeviceNum;

	InputDevice inputDevice = null;

	bool prevLeftTriggerPressed = false;
	bool prevRightTriggerPressed = false;

	// used to store the current orientation of the player
	// that way if the player isn't pressing the right stick,
	// the direction doesn't jump back to the default, it stays
	// where it was
	Vector3 direction;

	void Start() {
		if (InputManager.Devices.Count > inputDeviceNum) {
			inputDevice = InputManager.Devices[0];
		}
	}

	void LateUpdate() {
		if (inputDevice == null) {
			PrintErrorMessage();
			return;
		}
		prevLeftTriggerPressed = inputDevice.LeftTrigger > 0;
		prevRightTriggerPressed = inputDevice.RightTrigger > 0;
	}

	void PrintErrorMessage() {
		print("Error: No controller for input number " + inputDeviceNum + ". Plug in a controller or press F1 to enter dev mode");
	}

	public Vector3 GetDirection() {
		if (GameManager.instance.devMode) { // use keyboard & mouse
			return GetMousePosition() - transform.position;
		}

		// use controller
		if (inputDevice == null) {
			PrintErrorMessage();
			return Vector3.zero;
		}

		if (inputDevice.RightStickX != 0 || inputDevice.RightStickY != 0) {
			direction =  new Vector3(inputDevice.RightStickX, inputDevice.RightStickY, 0);
		}

		return direction;
	}

	static Vector3 GetMousePosition() {
		Vector3 v = Vector3.zero;

		Plane p = new Plane(Vector3.back, Vector3.zero);
		Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
		float distance = 0;
		if (p.Raycast(r, out distance)) {
			v = r.GetPoint(distance);
		}
		return v;
	}

	public Vector3 GetMovementDirection() {
        if (GameManager.instance.devMode) { // use keyboard & mouse
            return new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0).normalized;
        }

		// use controller
		if (inputDevice == null) {
			PrintErrorMessage();
			return Vector3.zero;
		}

		return new Vector3(inputDevice.LeftStick.X, inputDevice.LeftStick.Y, 0);
    }

	// returns true if the trigger was pressed down this frame
	public bool GetHandActionDown(int hand) {
		if (GameManager.instance.devMode) { // use keyboard & mouse
			return Input.GetMouseButtonDown(hand);
		}

		// use controller
		if (inputDevice == null) {
			PrintErrorMessage();
			return false;
		}

		switch (hand) {
			case 0:
				return inputDevice.LeftTrigger.Value > 0 && prevLeftTriggerPressed == false;
			case 1:
				return inputDevice.RightTrigger.Value > 0 && prevRightTriggerPressed == false;
		}
		return false;
	}

	// returns true if the trigger is down at all
	public bool GetHandActionHeld(int hand) {
		if (GameManager.instance.devMode) { // use keyboard & mouse
			return Input.GetMouseButton(hand);
		}

		// use controller
		if (inputDevice == null) {
			PrintErrorMessage();
			return false;
		}

		switch (hand) {
			case 0:
				return inputDevice.LeftTrigger.Value > 0;
			case 1:
				return inputDevice.RightTrigger.Value > 0;
		}
		return false;
	}
}
