using System;
using UnityEngine;

public class Hand : MonoBehaviour
{
	private void Awake()
	{
		this.rb = base.GetComponent<Rigidbody>();
		this.posHistory = new Vector3[this.bufferSize];
		for (int i = 0; i < this.posHistory.Length; i++)
		{
			this.posHistory[i] = Vector3.one;
		}
	}

	private void OnCollisionEnter(Collision other)
	{
		if (this.done)
		{
			return;
		}
		if (other.gameObject.layer != LayerMask.NameToLayer("Ground"))
		{
			return;
		}
		if (this.FindSpeed() < 0.75f)
		{
			return;
		}
		if (other.contacts.Length < 1)
		{
			return;
		}
		Quaternion quaternion = Quaternion.Euler(-90f, 0f, (float)Random.Range(0, 360));
		quaternion = Quaternion.LookRotation(other.contacts[0].normal);
		Object.Instantiate<GameObject>(this.groundHitFx, other.contacts[0].point + Vector3.up * 0.02f, quaternion);
		this.done = true;
		base.Invoke("StopHand", 0.3f);
	}

	private void FixedUpdate()
	{
		this.posHistory[this.tick % this.bufferSize] = base.transform.position;
		this.tick++;
	}

	public float FindSpeed()
	{
		float num = 0f;
		foreach (Vector3 vector in this.posHistory)
		{
			num += (vector - base.transform.position).magnitude;
		}
		return num;
	}

	private void StopHand()
	{
		this.done = false;
	}

	public GameObject groundHitFx;

	private Rigidbody rb;

	private bool done;

	private Vector3[] posHistory;

	private int bufferSize = 5;

	private int tick;
}
