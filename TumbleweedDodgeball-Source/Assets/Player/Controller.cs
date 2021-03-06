﻿using UnityEngine;
using System.Collections;
using InControl;
//using XInputDotNetPure;

public class Controller : MonoBehaviour {

    static bool devMode = false;
    static public bool canMove = false;

    public int inputDeviceNum;

    InputDevice inputDevice = null;

	bool prevLeftTriggerPressed = false, prevRightTriggerPressed = false,
		confirmPressed = false, zeroInput = false, backPressed = false;

    // used to store the current orientation of the player
    // that way if the player isn't pressing the right stick,
    // the direction doesn't jump back to the default, it stays
    // where it was
    Vector3 direction = new Vector3(1, 0, 0);

	static bool locked;

    void Start() {
        if (InputManager.Devices.Count > inputDeviceNum) {
            inputDevice = InputManager.Devices[inputDeviceNum];
			prevLeftTriggerPressed = inputDevice.LeftBumper.IsPressed;
			prevRightTriggerPressed = inputDevice.RightBumper.IsPressed;
			confirmPressed = inputDevice.Action1;
		}
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.F1) && inputDeviceNum >= 0)
            devMode = !devMode;
    }

    void LateUpdate() {
        if (inputDevice == null) {
            if (!devMode) {
                PrintErrorMessage();
                return;
            }
        }
        else {
            prevLeftTriggerPressed = inputDevice.LeftBumper.IsPressed;
            prevRightTriggerPressed = inputDevice.RightBumper.IsPressed;
			confirmPressed = inputDevice.Action1;
			backPressed = inputDevice.Action2;
			if (inputDevice.LeftStick.X == 0 && inputDevice.LeftStick.Y == 0) {
				zeroInput = true;
			}
			else {
				zeroInput = false;
			}
        }
    }

    void PrintErrorMessage() {
        //print("Error: No controller for input number " + inputDeviceNum + ". Plug in a controller or press F1 to enter dev mode");
    }

	public Vector2 GetMainDirection() {
		if (inputDevice == null) {
			PrintErrorMessage();
			return Vector2.zero;
		}

		if (locked) {
			return Vector2.zero;
		}

		if (!zeroInput) {
			return Vector2.zero;
		}

		float x = inputDevice.LeftStick.X;
		float y = inputDevice.LeftStick.Y;

		if (Mathf.Abs(x) > Mathf.Abs(y)) {
			return new Vector2(x, 0);
		}
		else {
			return new Vector2(0, y);
		}
	}

    public Vector3 GetDirection() {
        if (!canMove) return Vector3.zero; //player is frozen

		if (locked) {
			return Vector3.zero;
		}

		if (devMode || InputManager.Devices.Count <= inputDeviceNum) { // use keyboard & mouse
            return GetMousePosition() - transform.position;
        }

        // use controller
        if (inputDevice == null) {
            PrintErrorMessage();
            return Vector3.zero;
        }

        if (inputDevice.RightStickX != 0 || inputDevice.RightStickY != 0) {
            direction = new Vector3(inputDevice.RightStickX, inputDevice.RightStickY, 0);
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
        if (!canMove) return Vector3.zero; //Player is frozen

		if (locked) {
			return Vector3.zero;
		}

		if (devMode || InputManager.Devices.Count <= inputDeviceNum) { // use keyboard & mouse
            Vector3 dir = Vector3.zero;
            if (Input.GetKey(KeyCode.W))
                dir += Vector3.up;
            if (Input.GetKey(KeyCode.S))
                dir += Vector3.down;
            if (Input.GetKey(KeyCode.A))
                dir += Vector3.left;
            if (Input.GetKey(KeyCode.D))
                dir += Vector3.right;
            dir = dir.normalized;
            return dir;
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

		if (locked) {
			return false;
		}

		if (devMode || InputManager.Devices.Count <= inputDeviceNum) { // use keyboard & mouse
            return Input.GetMouseButtonDown(hand);
        }

        // use controller
        if (inputDevice == null) {
            PrintErrorMessage();
            return false;
        }

        switch (hand) {
            case 0:
                return inputDevice.LeftBumper.IsPressed && !prevLeftTriggerPressed;
            case 1:
                return inputDevice.RightBumper.IsPressed && !prevRightTriggerPressed;
        }
        return false;
    }

    // returns true if the trigger is down at all
    public bool GetHandActionHeld(int hand) {

		if (locked) {
			return false;
		}

		if (devMode || InputManager.Devices.Count <= inputDeviceNum) { // use keyboard & mouse
            return Input.GetMouseButton(hand);
        }

        // use controller
        if (inputDevice == null) {
            PrintErrorMessage();
            return false;
        }

        switch (hand) {
            case 0:
                return inputDevice.LeftBumper.IsPressed;
            case 1:
                return inputDevice.RightBumper.IsPressed;
        }
        return false;
    }

    public bool GetRestartPressed() {

		if (locked) {
			return false;
		}

		if (inputDevice == null)
			return Input.GetMouseButton(2);
        return inputDevice.Action2;
    }

	public void Vibrate(float intensity) {
		return;
//		if (!DebugManager.vibrationsEnabled)
//			return;
//
//		//Debug.Log(inputDevice.Meta);
//
//		inputDevice.Vibrate(intensity);
//
//		switch (inputDeviceNum) {
//			case 0:
//				GamePad.SetVibration(0, intensity, intensity);
//				break;
//			case 1:
//				GamePad.SetVibration((PlayerIndex)1, intensity, intensity);
//				break;
//			case 2:
//				GamePad.SetVibration((PlayerIndex)2, intensity, intensity);
//				break;
//			case 3:
//				GamePad.SetVibration((PlayerIndex)3, intensity, intensity);
//				break;
//		}
	}

	public bool GetConfirmDown() {

		if (locked) {
			return false;
		}

		if (devMode || InputManager.Devices.Count <= inputDeviceNum) { // use keyboard & mouse
			return Input.GetMouseButtonDown(2);
		}

		// use controller
		if (inputDevice == null) {
			PrintErrorMessage();
			return false;
		}

		return inputDevice.Action1 && !confirmPressed;
	}

	public bool GetDash() {

		if (locked) {
			return false;
		}

		if (!canMove)
			return false; //player is frozen
		if (devMode || InputManager.Devices.Count <= inputDeviceNum) {
			return Input.GetKey(KeyCode.B);
		}


		// use controller
		if (inputDevice == null) {
			PrintErrorMessage();
			return false;
		}

		return inputDevice.Action1;
	}

	public bool GetBackDown() { // B button

		if (locked) {
			return false;
		}

		if (devMode || InputManager.Devices.Count <= inputDeviceNum) {// use keyboard & mouse
			return Input.GetKey(KeyCode.B);
		}

		if (inputDevice == null) {
			PrintErrorMessage();
			return false;
		}

		return inputDevice.Action2 && !backPressed;
	}

	public static void Lock() {
		locked = true;
	}

	public static void Unlock() {
		locked = false;
	}

}

