using UnityEngine;
using System.Collections;

public class RotateModel : MonoBehaviour {
	
	void Update () {
		transform.Rotate(0,0,20*Time.deltaTime);
	}
}
