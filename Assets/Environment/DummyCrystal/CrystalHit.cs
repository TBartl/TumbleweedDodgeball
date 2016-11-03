using UnityEngine;
using System.Collections;

public class CrystalHit : Hittable{
	public GameObject shardsPrefab;

	public override void Hit()
	{
		base.Hit();
		Destroy(this.gameObject);
		Instantiate(shardsPrefab, this.transform.position, Quaternion.identity);
	}
}
