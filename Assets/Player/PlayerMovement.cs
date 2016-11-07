using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour {

	public float maxSpeed;
	Rigidbody2D rb;

	public List<float> modifiers;

    PlayerData playerData;
	Controller controller;

	public Material[] playerMats = new Material[4];

	void Awake()
	{
		rb = this.GetComponent<Rigidbody2D>();
	}

    void Start() {
        playerData = GetComponent<PlayerData>();
		controller = GetComponent<Controller>();
		GetComponentInChildren<Renderer>().material = playerMats[playerData.playerNum];
    }

	void FixedUpdate () {
		Vector3 direction = controller.GetMovementDirection();

		float speed = maxSpeed;
		foreach (float f in modifiers)
			speed *= f;
		rb.velocity = direction * speed;
	}

}
