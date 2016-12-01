using UnityEngine;
using System.Collections;

public class TrainMovement : MonoBehaviour {

	public float speed;
		//, timeBetween, offscreenY;
	//float startTime;
	//bool running = false;
	//public Vector3 startPos;
	Vector3[] path;
	float percent = 0;
	public string pathName;


	// Use this for initialization
	void Start () {
		path = iTweenPath.GetPath(pathName);
		iTween.PutOnPath(gameObject, path, percent);
		//startTime = Time.time + timeBetween;
	}
	
	// Update is called once per frame
	void Update () {
		percent += speed * Time.deltaTime;
		if (percent > 1) {
			percent -= 1;
		}
		iTween.PutOnPath(gameObject, path, percent);
		//if (running) {
		//	transform.position = new Vector3(transform.position.x, 
		//		transform.position.y - speed * Time.deltaTime, transform.position.z);
		//	if (transform.position.y <= offscreenY) {
		//		running = false;
		//		startTime = Time.time + timeBetween;
		//	}
		//}
		//else {
		//	if (Time.time >= startTime) {
		//		running = true;
		//		transform.position = startPos;
		//	}
		//}
	}
}
