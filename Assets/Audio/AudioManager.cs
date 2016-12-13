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
		destructibleBreak, wind, punch, moo, powerup, trainWhistle,
		gamePlayMusic, menuMusic, timer, tick, confirm, back, crash;

	public enum SceneType { MENU, GAMEPLAY }

	public SceneType type;
	AudioSource musicSource;

	void Awake() {
		if (instance == null) {
			instance = this;
			Init();
		}

		if (instance.type != type) { // switching from menu to gameplay or vice versa
			instance.type = type;
			instance.SetMusic();
		}

		if (instance != this) {
			Destroy(gameObject);
		}
	}

	void Init() {
		musicSource = GetComponent<AudioSource>();
		SetMusic();
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

	void SetMusic() {
		switch (type) {
			case SceneType.MENU:
				musicSource.clip = menuMusic.clip;
				musicSource.volume = menuMusic.volume;
				StartMusic();
				break;
			case SceneType.GAMEPLAY:
				musicSource.clip = gamePlayMusic.clip;
				musicSource.volume = gamePlayMusic.volume;
				break;
		}
	}

	public void StartMusic() {
		musicSource.Play();
	}
	
}
