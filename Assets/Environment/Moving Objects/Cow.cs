using UnityEngine;
using System.Collections;

public class Cow : MonoBehaviour {

	public float speed;
	Vector3[] path;
	float percent = 0;

	// Use this for initialization
	void Start () {
		path = iTweenPath.GetPath("Cow Path");
	}
	
	// Update is called once per frame
	void Update () {
		percent += speed * Time.deltaTime;
		if (percent > 1) {
			percent -= 1;
		}
		iTween.PutOnPath(gameObject, path, percent);
	}
}
