using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour {

	public float maxSpeed;
	Rigidbody2D rb;

	public List<float> modifiers;

    PlayerData playerData;

	void Awake()
	{
		rb = this.GetComponent<Rigidbody2D>();
	}

    void Start() {
        playerData = GetComponent<PlayerData>();
    }

	void FixedUpdate () {
        Vector3 direction = Controller.GetMovementDirection(playerData.playerNum);

		float speed = maxSpeed;
		foreach (float f in modifiers)
			speed *= f;
		rb.velocity = direction * speed;
	}


}
