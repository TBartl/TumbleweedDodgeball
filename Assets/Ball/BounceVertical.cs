using UnityEngine;
using System.Collections;

public class BounceVertical : MonoBehaviour {
	public float gravity;
	public float bouncePercent;
	float velocity;

	void Update()
	{
		float z = transform.localPosition.z;
		z += velocity * Time.deltaTime;

		if (z > 0)
		{
			z *= -bouncePercent;
			velocity *= -bouncePercent;
		}

		this.transform.position = new Vector3(transform.position.x, this.transform.position.y, z);

		velocity += gravity * Time.deltaTime;
	}

	public void Reset()
	{
		velocity = 0;
	}


}
