using UnityEngine;
using System.Collections;

public class CowDirection : MonoBehaviour {

	Vector3 prevPosition;
	CowMoovement moovement;

	// Use this for initialization
	void Start () {
		prevPosition = transform.position;
		moovement = GetComponent<CowMoovement>();
	}

	// Update is called once per frame
	void Update () {
		if (moovement.currentSpeed != 0 && transform.position != prevPosition) {
			Quaternion rotation = Quaternion.LookRotation(transform.position - prevPosition, Vector3.up);
			rotation.x = 0;
			rotation.y = 0;
			transform.rotation = rotation;
			prevPosition = transform.position;
		}
	}
}
