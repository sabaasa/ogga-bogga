using System;
using UnityEngine;

public class RandomSfx : MonoBehaviour
{
	private void Awake()
	{
		AudioSource component = base.GetComponent<AudioSource>();
		component.clip = this.sounds[Random.Range(0, this.sounds.Length - 1)];
		component.pitch = Random.Range(this.minPitch, this.maxPitch);
		component.Play();
	}

	public AudioClip[] sounds;

	[Range(0f, 2f)]
	public float maxPitch;

	[Range(0f, 2f)]
	public float minPitch;
}
