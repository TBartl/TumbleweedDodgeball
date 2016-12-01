using UnityEngine;
using System.Collections;

public class ItemSpawner : MonoBehaviour {

	public GameObject itemPrefab;
	public Vector3 offset;
	public float timeBetweenDropsMin, timeBetweenDropsMax;
	float nextDropTime;
	public Vector2 initialForce;
	// only spawns items when within spawnBounds
	public Bounds spawnBounds;

	// Use this for initialization
	void Start () {
		GenerateNextDropTime();
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time >= nextDropTime) {
			Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, 0)
				+ transform.rotation * offset;
			if (spawnBounds.Contains(newPosition)) {
				GameObject item = Instantiate(itemPrefab);
				item.transform.position = newPosition;
				Rigidbody2D itemBody = item.GetComponent<Rigidbody2D>();
				if (itemBody != null) {
					itemBody.AddForce(transform.rotation * initialForce);
				}
			}
			GenerateNextDropTime();
		}
	}

	void GenerateNextDropTime() {
		nextDropTime = Time.time + Random.Range(timeBetweenDropsMin, timeBetweenDropsMax);
	}
}
