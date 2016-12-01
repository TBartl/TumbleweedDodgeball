using UnityEngine;
using System.Collections;

public class GrowingScoreBarManager : MonoBehaviour {

    float growthFrameToFrame = 0.1f;

	public IEnumerator GrowToPos(float maxYPos) {
        while(transform.position.y < maxYPos) {
            Vector3 grown = new Vector3(transform.position.x, transform.position.y + growthFrameToFrame, transform.position.z);
            transform.position = grown;
            yield return null;
        }
    }
}
