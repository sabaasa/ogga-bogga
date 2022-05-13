using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	private void OnCollisionEnter(Collision other)
	{
		Object.Destroy(base.gameObject);
		Breakable component = other.gameObject.GetComponent<Breakable>();
		if (component)
		{
			component.Break();
		}
		if (other.contacts.Length < 1)
		{
			return;
		}
		Object.Instantiate<GameObject>(this.bulletExplosion, other.contacts[0].point, Quaternion.LookRotation(other.contacts[0].normal));
		GameObject gameObject = Object.Instantiate<GameObject>(this.bulletExplosion, other.contacts[0].point, Quaternion.LookRotation(other.contacts[0].normal));
		Renderer component2 = other.gameObject.GetComponent<Renderer>();
		if (!component2)
		{
			return;
		}
		Material material = component2.material;
		gameObject.GetComponent<ParticleSystemRenderer>().material = material;
	}

	public GameObject bulletExplosion;
}
