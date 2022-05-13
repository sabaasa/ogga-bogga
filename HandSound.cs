using System;
using UnityEngine;

public class HandSound : MonoBehaviour
{
	private void Awake()
	{
		this.handScript = base.GetComponent<Hand>();
		this.audio = base.GetComponent<AudioSource>();
	}

	private void Update()
	{
		float num = this.handScript.FindSpeed() * 0.6f;
		num = Mathf.Clamp(num, 0f, 1f);
		this.audio.volume = num;
	}

	private Hand handScript;

	private AudioSource audio;
}
