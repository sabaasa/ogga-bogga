using System;
using UnityEngine;

public class Splat : MonoBehaviour
{
	private void Awake()
	{
		this.enemy = base.GetComponent<Enemy>();
	}

	private void OnCollisionEnter(Collision other)
	{
		if (this.done)
		{
			return;
		}
		int layer = other.gameObject.layer;
		if (layer != LayerMask.NameToLayer("Ground") && layer != LayerMask.NameToLayer("Other"))
		{
			return;
		}
		if (other.gameObject.name == "Head")
		{
			return;
		}
		if (!this.fallDamage && !this.enemy.isRagdoll && layer == LayerMask.NameToLayer("Ground"))
		{
			return;
		}
		if (layer == LayerMask.NameToLayer("Other"))
		{
			Rigidbody component = other.gameObject.GetComponent<Rigidbody>();
			if (component && !component.isKinematic)
			{
				this.enemy.GrabEnemy();
			}
		}
		if (other.relativeVelocity.magnitude < 5f)
		{
			return;
		}
		GameObject ragdollObject = base.GetComponent<Enemy>().ragdollObject;
		if (ragdollObject)
		{
			Object.Destroy(ragdollObject);
		}
		Object.Instantiate<GameObject>(this.bloodSplat, base.transform.position, Quaternion.LookRotation(other.contacts[0].normal));
		ParticleSystem component2 = Object.Instantiate<GameObject>(this.otherFx, base.transform.position, Quaternion.LookRotation(other.contacts[0].normal)).GetComponent<ParticleSystem>();
		Renderer component3 = other.gameObject.GetComponent<Renderer>();
		if (component3 == null)
		{
			return;
		}
		Material material = component3.materials[0];
		if (material)
		{
			component2.gameObject.transform.localScale *= 0.25f;
			component2.gameObject.GetComponent<ParticleSystemRenderer>().material = material;
		}
		Object.Destroy(base.gameObject);
		this.done = true;
	}

	public GameObject bloodSplat;

	public GameObject otherFx;

	private bool done;

	public bool fallDamage = true;

	private Enemy enemy;
}
