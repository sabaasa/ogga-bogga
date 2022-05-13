using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Head : MonoBehaviour
{
	private void Awake()
	{
		this.audioSource = base.GetComponent<AudioSource>();
	}

	private void OnCollisionEnter(Collision other)
	{
		if (!this.ready)
		{
			return;
		}
		if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
		{
			Joint component = other.transform.root.gameObject.GetComponent<Joint>();
			if (!component)
			{
				return;
			}
			this.ready = false;
			Object.Destroy(component.connectedBody.gameObject);
			Object.Destroy(other.transform.root.gameObject);
			Object.Instantiate<GameObject>(this.bloodFx, other.transform.position, Quaternion.LookRotation(base.transform.forward));
			Player.Instance.Damage(-15f, 1f);
			this.audioSource.Play();
		}
		base.Invoke("Reset", Time.deltaTime);
	}

	private void Reset()
	{
		this.ready = true;
	}

	public GameObject bloodFx;

	public XRInteractionManager manager;

	private bool ready = true;

	private AudioSource audioSource;
}
