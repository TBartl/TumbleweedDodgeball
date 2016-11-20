using UnityEngine;
using System.Collections;

public class ScoreNumBehavior : MonoBehaviour {

    public void HandleScoreChange() {
        StartCoroutine(AnimateScore());
    }

    //decide on behavior for score, maybe it floats up or something
    IEnumerator AnimateScore() {
        float creationTime = Time.time;
        //show number for 1 second
        while (Time.time - creationTime < 1f) yield return null; 
        Destroy(this.gameObject);
    }
}
