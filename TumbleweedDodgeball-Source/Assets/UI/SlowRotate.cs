using UnityEngine;
using System.Collections;

public class SlowRotate : MonoBehaviour {

	float rotationSpeed = 20;
	float currentRotation = 0;
	
	// Update is called once per frame
	void Update () {
		transform.rotation = Quaternion.AngleAxis(currentRotation, new Vector3(0, 0, 1));
		currentRotation += rotationSpeed * Time.deltaTime;
		if (currentRotation >= 360) {
			currentRotation -= 360;
		}
	}
}
