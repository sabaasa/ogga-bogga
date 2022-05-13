using System;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabStone : MonoBehaviour
{
	private void Awake()
	{
		this.interactor = base.transform.parent.GetComponent<XRDirectInteractor>();
	}

	private void OnCollisionStay(Collision other)
	{
		if (!other.gameObject.CompareTag("StoneGrab"))
		{
			return;
		}
		InputDevices.GetDeviceAtXRNode(this.inputSource).TryGetFeatureValue(CommonUsages.grip, ref this.gripping);
		if (this.gripping < 0.1f || this.grabbingLastFrame)
		{
			return;
		}
		GameObject gameObject = Object.Instantiate<GameObject>(this.rock, base.transform.position, Quaternion.identity);
		Object.Instantiate<GameObject>(this.rockFx, base.transform.position, this.rockFx.transform.rotation);
		gameObject.transform.localScale *= Random.Range(0.9f, 1.35f);
		this.interactable = gameObject.GetComponent<XRGrabInteractable>();
		this.grabbingLastFrame = true;
	}

	private void LateUpdate()
	{
		InputDevices.GetDeviceAtXRNode(this.inputSource).TryGetFeatureValue(CommonUsages.grip, ref this.gripping);
		this.grabbingLastFrame = (this.gripping > 0.1f);
	}

	public GameObject rock;

	public GameObject rockFx;

	public XRNode inputSource;

	private XRDirectInteractor interactor;

	private XRGrabInteractable interactable;

	private float gripping;

	private bool grabbingLastFrame;
}
