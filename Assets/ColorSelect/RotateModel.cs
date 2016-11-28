using UnityEngine;
using System.Collections;

public class RotateModel : MonoBehaviour {

	public float amount;
	public float speed;

	void Update () {
		float val = Mathf.Sin(Time.time * speed);
		this.transform.rotation = Quaternion.Euler(0,amount * val,0);
	}
}
