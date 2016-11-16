using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	public Transform cameraTransform;

	Transform[] playerTransforms;

	// Use this for initialization
	void Start () {
		cameraTransform = GetComponent<Camera>().transform;
		playerTransforms = new Transform[PlayerManager.inst.players.Count];
		for (int i = 0; i < PlayerManager.inst.players.Count; ++i) {
			playerTransforms[i] = PlayerManager.inst.players[i].transform;
		}
	}
	
	// Update is called once per frame
	void Update () {
		cameraTransform.position = AveragePositions();
	}

	Vector3 AveragePositions() {
		Vector3 average = Vector3.zero;
		foreach (Transform transform in playerTransforms) {
			average += transform.position;
		}
		average /= playerTransforms.Length;
		average.z = cameraTransform.position.z;
		return average;
	}
}
