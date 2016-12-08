using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum StatusIconType {
	scoreLeader,
	dazed,
	powerup
}

[System.Serializable]
public class StatusIconVisuals {
	public Mesh mesh;
	public Material material;
}

[System.Serializable]
public class StatusIconPoolGO {
	public Transform t;
	public MeshRenderer mr;
	public MeshFilter mf;
}

public class PlayerStatusIcons : MonoBehaviour {
	public List<StatusIconVisuals> visuals;
	List<StatusIconPoolGO> pool = new List<StatusIconPoolGO>();
	List<StatusIconType> statuses = new List<StatusIconType>();

	void ExpandPool() {
		GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
		go.transform.position = this.transform.position;
		go.transform.rotation = this.transform.rotation;
		go.transform.parent = this.transform;
		Destroy(go.GetComponent<BoxCollider>());

		StatusIconPoolGO poolGO = new StatusIconPoolGO();
		poolGO.t = go.transform;
		poolGO.mr = go.GetComponent<MeshRenderer>();
		poolGO.mf = go.GetComponent<MeshFilter>();

		pool.Add(poolGO);
	}

	void UpdateIcons() {
		while (statuses.Count > pool.Count) {
			ExpandPool();
		}

		foreach (StatusIconPoolGO poolGO in pool) {
			poolGO.mr.enabled = false;
		}

		for (int i = 0; i < statuses.Count; i += 1) {
			StatusIconType type = statuses[i];

			pool[i].mr.enabled = true;
			pool[i].mr.material = visuals[(int)type].material;
			pool[i].mf.mesh = visuals[(int)type].mesh;
		}
	}


	public void AddStatus(StatusIconType status) {
		if (!statuses.Contains(status)) {
			statuses.Add(status);
			UpdateIcons();
		}
	}

	public void RemoveIcon(StatusIconType status) {
		if (statuses.Contains(status)) {
			statuses.Remove(status);
			UpdateIcons();
		}
	}

}
