using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrainMovement : MonoBehaviour {

	public float speed;
	List<Vector3[]> paths;
	float percent = 0;
	public string[] pathNames;
	int currentPathNum = 0;
	public float[] rotations;


	// Use this for initialization
	void Start () {
		paths = new List<Vector3[]>();
		foreach (string pathName in pathNames) {
			paths.Add(iTweenPath.GetPath(pathName));
		}
		iTween.PutOnPath(gameObject, paths[currentPathNum], percent);
	}
	
	// Update is called once per frame
	void Update () {
		percent += speed * Time.deltaTime;
		if (percent > 1) {
			percent -= 1;
			++currentPathNum;
			if (currentPathNum >= paths.Count) {
				currentPathNum = 0;
			}
			transform.rotation = Quaternion.Euler(0, 0, rotations[currentPathNum]);
		}
		iTween.PutOnPath(gameObject, paths[currentPathNum], percent);
	}
}
