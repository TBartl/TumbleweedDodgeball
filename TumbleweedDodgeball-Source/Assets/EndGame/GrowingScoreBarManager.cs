using UnityEngine;
using System.Collections;

public class GrowingScoreBarManager : MonoBehaviour {

    public GameObject fireworks;
    public float winningHeight;

    float growthFrameToFrame = 0.05f;

	public AudioSource fireworksAudio, drumRollAudio;

    public IEnumerator GrowToPos(float maxYPos) {
        while (transform.position.y < maxYPos) {
            Vector3 grown = new Vector3(transform.position.x, transform.position.y + growthFrameToFrame, transform.position.z);
            transform.position = grown;
            yield return null;
        }

		//if winning height, start fireworks
		if (maxYPos == winningHeight) {
			drumRollAudio.Stop();
			AudioManager.instance.PlayClip(AudioManager.instance.crash);
			fireworksAudio.mute = false;

			Vector3 posFireworks = new Vector3(this.transform.position.x, -4f, -10f);
			Quaternion rot = Quaternion.Euler(-90f, 0f, 0f);
			GameObject fireworks1 = (GameObject)Instantiate(fireworks, posFireworks, rot);

			//wait for 1.5 seconds and then start another fireworks
			yield return new WaitForSeconds(1.5f);
			GameObject fireworks2 = (GameObject)Instantiate(fireworks, posFireworks, rot);
		}
	}
}
