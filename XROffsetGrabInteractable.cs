using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XROffsetGrabInteractable : XRGrabInteractable
{
	private void Start()
	{
		if (!base.attachTransform)
		{
			GameObject gameObject = new GameObject("Grab Pivot");
			gameObject.transform.SetParent(base.transform, false);
			base.attachTransform = gameObject.transform;
		}
		this.initialAttachLocalPos = base.attachTransform.localPosition;
		this.initialAttachLocalRot = base.attachTransform.localRotation;
	}

	protected override void OnSelectEnter(XRBaseInteractor interactor)
	{
		if (interactor is XRDirectInteractor)
		{
			base.attachTransform.position = interactor.transform.position;
			base.attachTransform.rotation = interactor.transform.rotation;
		}
		else
		{
			base.attachTransform.localPosition = this.initialAttachLocalPos;
			base.attachTransform.localRotation = this.initialAttachLocalRot;
		}
		base.OnSelectEnter(interactor);
	}

	private Vector3 initialAttachLocalPos;

	private Quaternion initialAttachLocalRot;
}
