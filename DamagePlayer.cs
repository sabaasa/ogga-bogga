using System;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
	private void OnEnable()
	{
		this.done = false;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (this.done)
		{
			return;
		}
		if (other.gameObject.CompareTag("HurtBox"))
		{
			Player.Instance.Damage(10f, 1f);
			this.done = true;
		}
	}

	private bool done;
}
