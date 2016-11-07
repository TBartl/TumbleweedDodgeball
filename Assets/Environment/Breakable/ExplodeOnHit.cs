using UnityEngine;
using System.Collections;

public class ExplodeOnHit : Hittable{
	public GameObject shardsPrefab;

	public override void Hit(int source)
	{
		base.Hit(source);
		Destroy(this.gameObject);
		Instantiate(shardsPrefab, this.transform.position, Quaternion.identity);
	}
}
