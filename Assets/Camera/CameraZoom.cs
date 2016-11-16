using UnityEngine;
using System.Collections;

public class CameraZoom : MonoBehaviour {

	public float minZ, maxZ;
	public float minXSep, maxXSep, minYSep, maxYSep;

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

		if (xDif <= minXSep) {
			print(maxZ);
			return maxZ;
		}
		if (xDif >= maxXSep) {
			print(minZ);
			return minZ;
		}

		print((minXSep + xDif) * (minZ - maxZ) / (maxXSep - minXSep) - minZ);
		return (minXSep + xDif) * (minZ - maxZ) / (maxXSep - minXSep) - minZ;
	}
}
