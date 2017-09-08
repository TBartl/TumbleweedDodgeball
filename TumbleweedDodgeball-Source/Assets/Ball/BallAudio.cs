using UnityEngine;
using System.Collections;

public class BallAudio : MonoBehaviour {
	BallHotness hotness;

	void Start() {
		hotness = GetComponent<BallHotness>();
	}
	void OnCollisionEnter2D(Collision2D coll) {

		if (hotness.GetIsHot()) {
			switch (coll.collider.tag) {
				case "Player":
					AudioManager.instance.PlayClipAtPoint(AudioManager.instance.playerHit, transform.position);
					break;
				default:
					AudioManager.instance.PlayClipAtPoint(AudioManager.instance.otherHit, transform.position);
					break;
			}
		}
	}
}
