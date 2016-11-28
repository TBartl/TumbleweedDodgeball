using UnityEngine;
using System.Collections;

// Used for things like weak wood
public class ExplodeOnHit : Hittable{
	public GameObject shardsPrefab;

	public override void Hit(PlayerData source, Vector2 velocityHit)
	{
		base.Hit(source, velocityHit);
		Destroy(this.gameObject);
		Instantiate(shardsPrefab, this.transform.position, Quaternion.identity);
	}
}
