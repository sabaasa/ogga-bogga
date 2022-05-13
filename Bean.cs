using System;
using UnityEngine;

public class Bean : MonoBehaviour
{
	private void Awake()
	{
		base.Invoke("DestroySelf", 7f);
	}

	private void Explode()
	{
		Object.Destroy(base.gameObject);
		Object.Instantiate<GameObject>(this.beanFx, base.transform.position, Quaternion.identity);
	}

	private void OnCollisionEnter(Collision other)
	{
		this.Explode();
		MonoBehaviour.print("bean hit: " + other.gameObject.name);
		int layer = other.gameObject.layer;
		if (other.gameObject.name == "Head")
		{
			Player.Instance.Damage(20f, 1f);
		}
		if (other.gameObject.layer == LayerMask.NameToLayer("PhysicalHands"))
		{
			Player.Instance.Damage(10f, 1f);
		}
	}

	private void DestroySelf()
	{
		Object.Destroy(base.gameObject);
	}

	public GameObject beanFx;
}
