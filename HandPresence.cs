using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandPresence : MonoBehaviour
{
	private void Start()
	{
		this.TryGetController();
	}

	private void TryGetController()
	{
		List<InputDevice> list = new List<InputDevice>();
		InputDevices.GetDevicesWithCharacteristics(this.controllerCharacteristics, list);
		if (list.Count < 1)
		{
			return;
		}
		this.targetDevice = list[0];
		this.controller = Object.Instantiate<GameObject>(this.controllerPrefab, base.transform);
		this.controller.AddComponent<FixedJoint>().connectedBody = base.GetComponent<Rigidbody>();
		this.controller.transform.SetParent(null);
		this.animator = this.controller.GetComponent<Animator>();
	}

	private void Update()
	{
		if (!this.controller)
		{
			this.TryGetController();
			return;
		}
		bool flag;
		this.targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, ref flag);
		if (flag)
		{
			MonoBehaviour.print("pressing A");
		}
		this.HandAnimation();
	}

	private void HandAnimation()
	{
		float num;
		if (this.targetDevice.TryGetFeatureValue(CommonUsages.trigger, ref num))
		{
			this.animator.SetFloat("Trigger", num);
		}
		else
		{
			this.animator.SetFloat("Trigger", 0f);
		}
		float num2;
		if (this.targetDevice.TryGetFeatureValue(CommonUsages.grip, ref num2))
		{
			this.animator.SetFloat("Grip", num2);
			return;
		}
		this.animator.SetFloat("Grip", 0f);
	}

	public InputDeviceCharacteristics controllerCharacteristics;

	public GameObject controllerPrefab;

	private GameObject controller;

	private InputDevice targetDevice;

	private Animator animator;
}
