using UnityEngine;
using System.Collections;


public class PlayerDirection : MonoBehaviour {	
	// Update is called once per frame
	void Update () {
		Vector2 directionDiff = GetDirection();
		transform.rotation = Quaternion.Euler(0, 0, -90 + Mathf.Atan2(directionDiff.y, directionDiff.x) * Mathf.Rad2Deg);
	}

	public Vector3 GetDirection() {
		//TODO Insert code for right analog stick direction here
		return GetMousePosition() - this.transform.position;
	}

	Vector3 GetMousePosition() {
		Vector3 v = Vector3.zero;

		Plane p = new Plane(Vector3.back, Vector3.zero);
		Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
		float distance = 0;
		if (p.Raycast(r, out distance)) {
			v = r.GetPoint(distance);
		}
		return v;
	}
}
