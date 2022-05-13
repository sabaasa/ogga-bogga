using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SmashOnImpact : MonoBehaviour
{
	private void Awake()
	{
		this.rb = base.GetComponent<Rigidbody>();
		this.interactable = base.GetComponent<XRGrabInteractable>();
	}

	private void OnCollisionEnter(Collision other)
	{
		if (this.interactable.isSelected)
		{
			return;
		}
		if (other.relativeVelocity.magnitude > this.speedLimit)
		{
			Object.Destroy(base.gameObject);
			if (other.contacts.Length < 1)
			{
				return;
			}
			GameObject gameObject = Object.Instantiate<GameObject>(this.fx, other.contacts[0].point, Quaternion.LookRotation(other.contacts[0].normal));
			if (other.gameObject.layer != LayerMask.NameToLayer("Ground"))
			{
				Object.Destroy(gameObject.transform.GetChild(2).gameObject);
			}
		}
	}

	private Rigidbody rb;

	private XRGrabInteractable interactable;

	public float speedLimit;

	public GameObject fx;
}
