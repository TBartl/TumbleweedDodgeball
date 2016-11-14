using UnityEngine;
using System.Collections;

public class PlayerDaze : MonoBehaviour {
	PlayerData playerData;
	PlayerMovement movement;
	public MeshRenderer dazeIcon;

	public float moveModifier = .31f;
	public float duration = 2;

	float timeRemaining = 0;

	void Awake() {
		playerData = GetComponent<PlayerData>();
		movement = GetComponent<PlayerMovement>();
	}

	void Start() {
		dazeIcon.enabled = false;
	}

	void Update() {
		float newTime = timeRemaining - Time.deltaTime;
		if (timeRemaining > 0 && newTime <= 0)
			DeactivateDaze();
		timeRemaining -= Time.deltaTime;
	}

	void OnTriggerStay2D(Collider2D other) {
		if (other.tag == "Smack") {
			if (other.GetComponentInParent<PlayerData>() != playerData) {
				if (timeRemaining <= 0)
					ActivateDaze();
				timeRemaining = duration;
			}
		}
	}

	void ActivateDaze() {
		dazeIcon.enabled = true;
		movement.modifiers.Add(moveModifier);
	}
	void DeactivateDaze() {
		dazeIcon.enabled = false;
		movement.modifiers.Remove(moveModifier);
	}
}
