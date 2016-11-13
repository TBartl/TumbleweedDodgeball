using UnityEngine;
using System.Collections;

// Used for things like weak wood
public class ExplodeOnHit : Hittable{
	public GameObject shardsPrefab;

	public override void Hit(PlayerData source)
	{
		base.Hit(source);
		Destroy(this.gameObject);
		Instantiate(shardsPrefab, this.transform.position, Quaternion.identity);
	}
}
