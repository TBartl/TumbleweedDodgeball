using UnityEngine;
using System.Collections;

public class SpawnInRect : MonoBehaviour {
	public GameObject prefabToSpawn;
	public Vector2 area;
	public float timeBetweenSpawns;
	float timeSinceLastSpawn;

	public float acceleration;
	public float minSpeed;
		
	// Update is called once per frame
	void Update () {
		timeSinceLastSpawn += Time.deltaTime;
		if (timeSinceLastSpawn > timeBetweenSpawns)
		{
			timeSinceLastSpawn = 0;
			Instantiate(prefabToSpawn, 
				new Vector3(Random.Range(-area.x, area.x), Random.Range(-area.y, area.y), 0), 
				Quaternion.identity, this.transform);
		}
		timeBetweenSpawns = Mathf.Max(minSpeed, timeBetweenSpawns - acceleration * Time.deltaTime);
	
	}
}
