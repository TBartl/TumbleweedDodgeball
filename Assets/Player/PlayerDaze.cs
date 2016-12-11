using UnityEngine;
using System.Collections;

public class PlayerDaze : MonoBehaviour {
	PlayerData playerData;
	PlayerMovement movement;
	PlayerStatusIcons icons;
	CameraShake cameraShake;

	public AnimationCurve recoveryCurve;
	public float duration = 2;

	float remaining = 0;

	void Awake() {
		playerData = GetComponent<PlayerData>();
		movement = GetComponent<PlayerMovement>();
		icons = GetComponentInChildren<PlayerStatusIcons>();
	}

	void OnTriggerStay2D(Collider2D other) {
		if (other.tag == "Smack") {
			if (other.GetComponentInParent<PlayerData>() != playerData) {
				if (!AlreadyDazed()) {
					AudioManager.instance.PlayClipAtPoint(AudioManager.instance.playerHit, transform.position);
					CameraShake.S.StartShake(.5f);
				}
				Daze();
			}
		}
	}

	public void Daze() {
		bool alreadyDazed = AlreadyDazed();
		remaining = duration;
		if (!alreadyDazed) {
			StartCoroutine(DazeCoroutine());
		}
	}


	IEnumerator DazeCoroutine() {
		icons.AddStatus(StatusIconType.dazed);
		while (remaining > 0) {
			remaining -= Time.deltaTime;
			float percent = Mathf.Clamp01(1 - (remaining / duration));
			movement.dazeMovement = recoveryCurve.Evaluate(percent);
			yield return null;
		}
		movement.dazeMovement = 1f;
		icons.RemoveStatus(StatusIconType.dazed);
	}

	bool AlreadyDazed() {
		return remaining > 0;
	}
}
