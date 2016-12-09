using UnityEngine;
using System.Collections;

public class MoveCloud : MonoBehaviour {

	public Vector2 speedRange;
	float speed;
	public float maxDist = 80;

	// Use this for initialization
	void Start () {
		speed = Mathf.Lerp(speedRange.x, speedRange.y, Random.value);	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position += Vector3.right * speed * Time.deltaTime;
		if (transform.position.x > maxDist)
			this.transform.position += Vector3.left * maxDist * 2;
	
	}
}
