using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour {
	public float spawnInterval = 5f;
	float timeUntilSpawn;
	public Vector2 boxRadius;
	public List<GameObject> prefabs;

	void Start() {
		timeUntilSpawn = spawnInterval;
	}
	
	// Update is called once per frame
	void Update () {
		timeUntilSpawn -= Time.deltaTime;
		
		if (timeUntilSpawn <= spawnInterval) {
			GameObject prefab = prefabs[Random.Range(0, prefabs.Count)];
			Vector3 spawnLocation = new Vector3(boxRadius.x * Random.Range(-1f, 1f), boxRadius.y * Random.Range(-1f, 1f), 0);
			Quaternion spawnRotation = Quaternion.Euler(0,0 , Random.Range(0,360));
			Instantiate(prefab, spawnLocation, spawnRotation);
			timeUntilSpawn += spawnInterval;
		}
	
	}
}
