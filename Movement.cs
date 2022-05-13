using System;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Movement : MonoBehaviour
{
	private void Awake()
	{
		this.rig = base.GetComponent<XRRig>();
	}

	private void Update()
	{
		InputDevices.GetDeviceAtXRNode(this.inputSource).TryGetFeatureValue(CommonUsages.primary2DAxis, ref this.input);
		base.transform.position += new Vector3(-this.input.x, 0f, 0f) * this.speed * Time.deltaTime;
	}

	private void FixedUpdate()
	{
	}

	private XRNode inputSource = 4;

	private Vector2 input;

	private Rigidbody rb;

	private XRRig rig;

	private float speed = 2f;

	private float height = 0.73f;

	public Transform playerCollider;

	public Transform camera;
}
