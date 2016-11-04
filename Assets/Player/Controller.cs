using UnityEngine;
using System.Collections;
using InControl;

public static class Controller {

    public static Hashtable controlMapping = new Hashtable();

    public static Vector3 GetDirection(int playerNum) {

        if (GameManager.instance.devMode) { // use keyboard & mouse
            return GetMousePosition() - GameManager.instance.players[playerNum].transform.position;
        }

        // use controller
        InputDevice inputDevice = InputManager.Devices[(int) controlMapping[playerNum]];
        return new Vector3(inputDevice.RightStickX, inputDevice.RightStickY, 0);
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

    public static Vector3 GetMovementDirection(int playerNum) {
        if (GameManager.instance.devMode) { // use keyboard & mouse
            return new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0).normalized;
        }

        // use controller
        InputDevice inputDevice = InputManager.Devices[(int)controlMapping[playerNum]];
        return new Vector3(inputDevice.LeftStick.X, inputDevice.LeftStick.Y, 0);
    }

    public static bool GetHandAction(int playerNum, int hand) {
        if (GameManager.instance.devMode) { // use keyboard & mouse
            return Input.GetMouseButtonDown(hand);
        }

        // use controller
        InputDevice inputDevice = InputManager.Devices[(int)controlMapping[playerNum]];
        switch (hand) {
            // TODO might need to adjust the threshold
            case 0:
                return inputDevice.LeftTrigger > .1;
            case 1:
				MonoBehaviour.print(inputDevice.RightTrigger);
                return inputDevice.RightTrigger > .1;
        }
        return false;
    }
}
