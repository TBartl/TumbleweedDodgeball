using UnityEngine;
using System.Collections;

public class SlowGrowBall : Ball {

    public float maxScale;
    private bool wasThrown = false;
    private float originalScale;

	public float growRate, shrinkRate;
	float currentScale;

	//BallHotness hotness;

    void Start() {
        originalScale = this.transform.localScale.x;
		hotness = GetComponent<BallHotness>();
    }

    public override void Throw(Vector3 velocity) {
        wasThrown = true;
		StartCoroutine(Grow());
        base.Throw(velocity);
    }

	void Update() {
		if (wasThrown && !hotness.GetIsHot()) {
			Reset();
		}
	}

	IEnumerator Grow() {
		currentScale = 1;
		while (wasThrown && currentScale <= maxScale) {
			currentScale += growRate * Time.deltaTime;
			transform.localScale = Vector3.one * currentScale;
			yield return null;
		}
		currentScale = maxScale;
		transform.localScale = Vector3.one * currentScale;
	}

	IEnumerator Shrink() {
		while (!wasThrown && currentScale >= 1) {
			currentScale -= shrinkRate * Time.deltaTime;
			transform.localScale = Vector3.one * currentScale;
			yield return null;
		}
		currentScale = 1;
		transform.localScale = Vector3.one * currentScale;
	}

    void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.tag == "Player" && (col.gameObject.GetComponent<PlayerData>().num != GetComponent<BallSource>().GetThrower().num)) {
			Destroy(gameObject);
		}
    }

    void Reset() {
        wasThrown = false;
		//transform.localScale = new Vector3(originalScale, originalScale, originalScale);
		StartCoroutine(Shrink());
    }
}
