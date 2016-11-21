using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BallBoomerang : MonoBehaviour {

	private bool returning = false;
	private GameObject thrower;

	Rigidbody2D rigid;
	BallSource ballSource;

	void Awake() {
		rigid = this.GetComponent<Rigidbody2D>();
		ballSource = this.GetComponent<BallSource>();
	}
	

	void Update () {
		if (returning) {
			Vector3 dir = (PlayerManager.inst.players[ballSource.GetThrower().num].transform.position - this.transform.position).normalized;
			rigid.velocity = dir * rigid.velocity.magnitude;
		} else {
			//nothing for now
		}
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (returning) Destroy(this.gameObject);
		else {
			returning = true;
		}
	}
}
