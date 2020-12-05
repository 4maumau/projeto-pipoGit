using UnityEngine.Audio;
using System;
using UnityEngine;
using DG.Tweening;

public class AudioManager : MonoBehaviour
{
	public AudioMixer audioMixer;
	public bool audioOn = true;
	public bool musicOn = true;
	
	public static AudioManager instance;

	public AudioMixerGroup managerMixerGroup;
	
	Sound s;

	public Sound[] sounds;

	void Awake()
	{
		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}

		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;

			s.source.outputAudioMixerGroup = s.mixerGroup;
		}
	}

	public void PlaySound(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}


		s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

		if (s.playOneShot) s.source.PlayOneShot(s.source.clip, s.volume);
		else s.source.Play();

	}

	public void StopSound(string sound, float fadeDuration = 0f)
    {
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}
		s.source.DOFade(0, fadeDuration);

		s.source.Stop();
	}

	public Sound GetSound(string sound)
    {
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return null;
		}

		return s;
	}

	
}
