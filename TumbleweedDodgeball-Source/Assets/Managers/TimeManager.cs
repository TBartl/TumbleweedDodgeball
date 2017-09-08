using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour {
	public static TimeManager instance;

	void Awake () {
		if (instance == null)
			instance = this;
	}


}
