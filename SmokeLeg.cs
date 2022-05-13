﻿using System;
using UnityEngine;

public class SmokeLeg : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if (!this.ready)
		{
			return;
		}
		this.ready = false;
		base.Invoke("GetReady", this.cooldown);
		Object.Instantiate<GameObject>(this.smokeFx, base.transform.position, this.smokeFx.transform.rotation);
	}

	private void GetReady()
	{
		this.ready = true;
	}

	public GameObject smokeFx;

	public float cooldown;

	private bool ready = true;
}
