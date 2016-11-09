using UnityEngine;
using System.Collections;

public class BallHotness : MonoBehaviour {
	TrailRenderer tr;
	Rigidbody2D rb;
	BallSource source;

	public float threshold;
	bool isHot = false;

	float initialTrailWidth;
	float initialTrailTime;

	void Awake()
	{
		tr = this.GetComponentInChildren<TrailRenderer>();
		rb = this.GetComponent<Rigidbody2D>();
		source = this.GetComponent<BallSource>();

		initialTrailWidth = tr.startWidth;
		initialTrailTime = tr.time;
	}
	
	// Update is called once per frame
	void Update () {
		isHot = (rb.velocity.magnitude >= threshold);
		if (isHot)
			tr.startWidth = initialTrailWidth;
		else
			tr.startWidth = Mathf.Max(0, tr.startWidth - 2 * Time.deltaTime);//Hardcoded because yolo

		// If we're effectively at 0 velocity, don't record the trail 
		// so when we throw it the trail will start from the hands
		if (rb.velocity.magnitude <= .05f)
			tr.time = 0;
		else
			tr.time = initialTrailTime;

	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (isHot && (other.gameObject.tag == "Hittable" || other.gameObject.tag == "Player"))
		{
			other.gameObject.GetComponent<Hittable>().Hit(source.GetSourceID());
			this.OnHit(other.gameObject.tag);
		}
	}

	protected virtual void OnHit(string tag) {

	}

	public bool GetIsHot() {
		return isHot;
	}
}
