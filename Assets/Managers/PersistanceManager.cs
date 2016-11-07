using UnityEngine;
using System.Collections;

public class PersistanceManager : MonoBehaviour {
	static PersistanceManager instance;

	void Awake () {
		if (instance != null)
			Destroy(this.gameObject);
		else {
			instance = this;
			DontDestroyOnLoad(this.gameObject);
		}
	}
}
