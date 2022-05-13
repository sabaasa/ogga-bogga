using System;
using System.Collections.Generic;
using UnityEngine;

public class BounceObjects : MonoBehaviour
{
	private void Awake()
	{
		base.Invoke("DestroySelf", 0.3f);
		this.bounced = new List<GameObject>();
	}

	private void OnTriggerEnter(Collider other)
	{
		IBounce component = other.GetComponent<IBounce>();
		if (component == null)
		{
			return;
		}
		if (this.bounced.Contains(other.gameObject))
		{
			return;
		}
		this.bounced.Add(other.gameObject);
		component.Bounce();
	}

	private void DestroySelf()
	{
		Object.Destroy(base.gameObject);
	}

	private List<GameObject> bounced;
}
