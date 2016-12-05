using UnityEngine;
using System.Collections;

public class CameraZoom : MonoBehaviour {

	public float xOffset, yOffset;

	public float minZ, maxZ;

	public Transform cameraTransform;

	Transform[] playerTransforms;

	void Start() {
		cameraTransform = GetComponent<Camera>().transform;
		playerTransforms = new Transform[PlayerManager.inst.players.Count];
		for (int i = 0; i < PlayerManager.inst.players.Count; ++i) {
			playerTransforms[i] = PlayerManager.inst.players[i].transform;
		}
	}

	// Update is called once per frame
	void Update () {
		cameraTransform.position = new Vector3(cameraTransform.position.x, cameraTransform.position.y, getZ());
	}

	float getZ() {
		float minX = float.MaxValue, maxX = float.MinValue, minY = float.MaxValue, maxY = float.MinValue;
		foreach (Transform transform in playerTransforms) {

			minX = Mathf.Min(minX, transform.position.x);
			maxX = Mathf.Max(maxX, transform.position.x);

			minY = Mathf.Min(minY, transform.position.y);
			maxY = Mathf.Max(maxY, transform.position.y);
		}

		float xDif = Mathf.Abs(maxX - minX);
		float yDif = Mathf.Abs(maxY - minY);

		float z = -Mathf.Max((xDif / (2 * Mathf.Tan(GetComponent<Camera>().fieldOfView * Mathf.Deg2Rad)) * xOffset), (yDif / (2 * Mathf.Tan(GetComponent<Camera>().fieldOfView * Mathf.Deg2Rad)) * yOffset));

		z = Mathf.Max(z, minZ);
		z = Mathf.Min(z, maxZ);

		return z;
	}
}
