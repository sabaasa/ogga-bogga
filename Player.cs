using System;
using UnityEngine;

public class Player : MonoBehaviour
{
	private void Awake()
	{
		Player.Instance = this;
		this.hp = 100f;
		this.maxHp = 100f;
		this.audioSource = base.GetComponent<AudioSource>();
		Application.targetFrameRate = 120;
	}

	public void Damage(float d, float vignetteIntensity)
	{
		if (this.hp <= 0f)
		{
			return;
		}
		this.hp -= d;
		if (this.hp <= 0f)
		{
			this.Kill();
		}
		if (this.hp > this.maxHp)
		{
			this.hp = this.maxHp;
		}
		HPBar.Instance.UpdateHp(this.hp, this.maxHp, vignetteIntensity);
		if (d > 5f)
		{
			this.audioSource.Play();
		}
	}

	private void Update()
	{
		if (GameManager.Instance.gameDone)
		{
			return;
		}
		this.Damage(2f * Time.deltaTime, 0f);
	}

	private void Kill()
	{
		this.hp = 0f;
		GameManager.Instance.gameDone = true;
		GameManager.Instance.EndGame();
	}

	public bool IsDead()
	{
		return this.hp <= 0f;
	}

	private float hp;

	private float maxHp;

	private AudioSource audioSource;

	public static Player Instance;
}
