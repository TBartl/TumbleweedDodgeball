using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	public Transform cameraTransform;

	Transform[] playerTransforms;
	public float yOffset = -3.5f;

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
		cameraTransform.position = GetPosition();
	}

	Vector3 GetPosition() {

		float minX = float.MaxValue, maxX = float.MinValue, minY = float.MaxValue, maxY = float.MinValue;

		foreach (Transform transform in playerTransforms) {
			minX = Mathf.Min(minX, transform.position.x);
			maxX = Mathf.Max(maxX, transform.position.x);
			minY = Mathf.Min(minY, transform.position.y);
			maxY = Mathf.Max(maxY, transform.position.y);
		}

		return new Vector3((minX + maxX) / 2, (minY + maxY) / 2 + yOffset, cameraTransform.position.z);
	}
}
