using System;
using UnityEngine;
using UnityEngine.XR;

public class Grappler : MonoBehaviour
{
	private void Start()
	{
		this.lr = base.GetComponent<LineRenderer>();
		this.lr.positionCount = this.positions;
		this.audioSource = base.GetComponent<AudioSource>();
	}

	private void LateUpdate()
	{
		this.UpdateJoint();
		this.DrawRope();
		this.Input();
	}

	public void StartGrapple()
	{
		if (this.grappling)
		{
			return;
		}
		MonoBehaviour.print("grappling");
		this.point = this.FindGrapplingPoint();
		if (this.point == Vector3.zero)
		{
			return;
		}
		this.lr.enabled = true;
		this.lr.SetPosition(0, this.laserStartPos.position);
		this.lr.SetPosition(1, this.point);
		this.endPoint = this.laserStartPos.position;
		this.offsetMultiplier = 2f;
		this.joint = this.player.gameObject.AddComponent<SpringJoint>();
		this.joint.autoConfigureConnectedAnchor = false;
		this.joint.spring = 4.5f;
		this.joint.damper = 7f;
		this.joint.massScale = 4.5f;
		this.joint.connectedAnchor = this.point;
		this.joint.maxDistance = Vector3.Distance(this.point, this.player.position) * 0.8f;
		this.joint.minDistance = Vector3.Distance(this.point, this.player.position) * 0.25f;
		this.grappling = true;
	}

	private void UpdateJoint()
	{
		if (!this.grappling)
		{
			return;
		}
		if (this.point == Vector3.zero)
		{
			this.StopGrapple();
			return;
		}
	}

	public void StopGrapple()
	{
		if (!this.grappling)
		{
			return;
		}
		this.grappling = false;
		this.target = null;
		this.lr.enabled = false;
		this.targetIsEnemy = false;
		Object.Destroy(this.joint);
		this.point = Vector3.zero;
		MonoBehaviour.print("stopping grapple");
	}

	private void DrawRope()
	{
		if (!this.grappling)
		{
			return;
		}
		this.endPoint = Vector3.Lerp(this.endPoint, this.point, Time.deltaTime * 15f);
		this.offsetMultiplier = Mathf.SmoothDamp(this.offsetMultiplier, 0f, ref this.offsetVel, 0.1f);
		this.startPoint = this.laserStartPos.position;
		float num = Vector3.Distance(this.endPoint, this.startPoint);
		this.lr.SetPosition(0, this.startPoint);
		this.lr.SetPosition(this.positions - 1, this.endPoint);
		float num2 = num;
		float num3 = 1f;
		for (int i = 1; i < this.positions - 1; i++)
		{
			float num4 = (float)i / (float)this.positions;
			float num5 = num4 * this.offsetMultiplier;
			float num6 = (Mathf.Sin(num5 * num2) - 0.5f) * num3 * (num5 * 2f);
			Vector3 normalized = (this.endPoint - this.startPoint).normalized;
			float num7 = Mathf.Sin(num4 * 180f * 0.017453292f);
			float num8 = Mathf.Cos(this.offsetMultiplier * 90f * 0.017453292f);
			Vector3 vector = this.startPoint + (this.endPoint - this.startPoint) / (float)this.positions * (float)i + (num8 * num6 * Vector2.Perpendicular(normalized) + this.offsetMultiplier * num7 * Vector3.down);
			this.lr.SetPosition(i, vector);
		}
	}

	private void Input()
	{
		InputDevice deviceAtXRNode = InputDevices.GetDeviceAtXRNode(this.inputSource);
		deviceAtXRNode.TryGetFeatureValue(CommonUsages.primaryButton, ref this.pull);
		deviceAtXRNode.TryGetFeatureValue(CommonUsages.secondaryButton, ref this.push);
		if (this.pull)
		{
			this.joint.maxDistance *= 0.95f;
		}
		if (this.push)
		{
			this.joint.maxDistance *= 1.05f;
		}
	}

	private Vector3 FindGrapplingPoint()
	{
		if (this.grappling)
		{
			return Vector3.zero;
		}
		RaycastHit raycastHit;
		if (Physics.Raycast(this.gunTip.position, this.gunTip.right, ref raycastHit, 50f, this.whatIsGrappleLayers))
		{
			return raycastHit.point;
		}
		return Vector3.zero;
	}

	private LineRenderer lr;

	public LayerMask whatIsGrappleLayers;

	public Transform laserStartPos;

	private Transform target;

	public Transform gunTip;

	public Transform player;

	private Rigidbody rb;

	private Vector3 startPoint;

	private Vector3 endPoint;

	private float offsetMultiplier;

	private float offsetVel;

	private int positions = 50;

	private SpringJoint joint;

	private float spring = 4.5f;

	private float damper = 7f;

	private float massScale = 4.5f;

	private float minDist = 0.2f;

	private float maxDistance = 0.8f;

	private float grapplePullForce = 200f;

	public float maxGrappleSpeed = 50f;

	private AudioSource audioSource;

	[HideInInspector]
	public bool grappling;

	private bool targetIsEnemy;

	private XRNode inputSource = 5;

	public Transform objectThing;

	private Vector3 point;

	private bool pull;

	private bool push;
}
