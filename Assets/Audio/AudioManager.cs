using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour {

	[System.Serializable]
	public class AudioClipWithVolume {
		public AudioClip clip;
		public float volume;
	}

	public static AudioManager instance;

	[SerializeField]
	public AudioClipWithVolume throwSound, playerHit, otherHit,
		destructibleBreak, wind, punch, moo, powerup, trainWhistle;

	void Awake() {
		if (instance == null) {
			instance = this;
		}

		if (instance != this) {
			Destroy(gameObject);
		}
	}

	public void PlayClipAtPoint(AudioClipWithVolume clipWithVolume, Vector3 point) {
		AudioSource.PlayClipAtPoint(clipWithVolume.clip, point, clipWithVolume.volume);
	}

	public void PlayClip(AudioClipWithVolume clipWithVolume) {
		StartCoroutine(PlayAndDestroy(clipWithVolume));
	}

	IEnumerator PlayAndDestroy(AudioClipWithVolume clipWithVolume) {
		AudioSource source = gameObject.AddComponent<AudioSource>();
		source.clip = clipWithVolume.clip;
		source.volume = clipWithVolume.volume;
		source.Play();
		while (source.isPlaying) {
			yield return null;
		}
		Destroy(source);
	}
	
}
