using System;
using UnityEngine;

public class RockBounce : MonoBehaviour, IBounce
{
	public void Bounce()
	{
		Rigidbody component = base.GetComponent<Rigidbody>();
		component.isKinematic = false;
		base.transform.position += Vector3.up * 0.2f;
		component.AddForce(Vector3.up * 80f, 1);
		component.AddTorque(new Vector3(Random.Range(-1f, 1f), Random.Range(-0.2f, 0.2f), Random.Range(-1f, 1f)) * 4f, 1);
	}
}
