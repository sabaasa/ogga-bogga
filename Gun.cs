using System;
using UnityEngine;

public class Gun : MonoBehaviour
{
	private void Awake()
	{
		this.gunCollider = base.GetComponent<Collider>();
		this.lineRenderer = base.GetComponent<LineRenderer>();
	}

	public void Shoot()
	{
		GameObject gameObject = Object.Instantiate<GameObject>(this.bullet, this.gunTip.position, this.gunTip.rotation);
		this.RemoveCollisionWithGun(gameObject.GetComponent<Collider>(), this.gunCollider);
		Vector3 right = this.gunTip.right;
		gameObject.GetComponent<Rigidbody>().AddForce(right * this.bulletSpeed);
		Object.Instantiate<GameObject>(this.muzzle, this.gunTip.position + this.gunTip.right * 0.1f, this.gunTip.rotation);
	}

	private void RemoveCollisionWithGun(Collider bulletCollider, Collider gunCollider)
	{
		Physics.IgnoreCollision(bulletCollider, gunCollider, true);
	}

	private void Update()
	{
	}

	public GameObject bullet;

	public Transform gunTip;

	public GameObject muzzle;

	public float bulletSpeed;

	private Collider gunCollider;

	private LineRenderer lineRenderer;
}
