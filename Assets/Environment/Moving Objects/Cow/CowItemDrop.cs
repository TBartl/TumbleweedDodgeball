using UnityEngine;
using System.Collections;

public class CowItemDrop : MonoBehaviour {

	public GameObject ballPrefab;
	public float width;

	float nextDropTime;
	Mesh mesh;

	public float minDuration, maxDuration;

	// Use this for initialization
	void Start () {
		GenerateNextDropTime();
		mesh = GetComponent<Mesh>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time >= nextDropTime) {
			GameObject ball = Instantiate(ballPrefab);
			ball.transform.position = new Vector3(transform.position.x, transform.position.y, 1) 
				- transform.rotation * new Vector3(width / 2, 0, 0);
			GenerateNextDropTime();
		}
	}

	void GenerateNextDropTime() {
		nextDropTime = Time.time + Random.Range(minDuration, maxDuration);
	}
}
