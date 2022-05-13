using System;
using UnityEngine;

public class Breakable : MonoBehaviour
{
	public void Break()
	{
		Object.Destroy(base.gameObject);
		Object.Instantiate<GameObject>(this.breakFx, base.transform.position, Quaternion.identity);
	}

	private void OnCollisionEnter(Collision other)
	{
		if (other.relativeVelocity.magnitude > 5f)
		{
			this.Break();
			if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
			{
				Rigidbody component = other.transform.root.GetComponent<RigidEnemy>().root.GetComponent<Rigidbody>();
				if (component)
				{
					component.AddForce(250f * -other.relativeVelocity);
				}
			}
		}
	}

	public GameObject breakFx;
}
