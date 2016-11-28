using UnityEngine;
using System.Collections;

public class OutOfBoundsReset : MonoBehaviour {
	public float radius = 20f;
	Vector3 initialPos;

	// Use this for initialization
	void Start () {
		initialPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance(Vector3.zero, transform.position) >= radius)
			this.transform.position = initialPos;
	}
}
