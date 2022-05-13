using System;
using UnityEngine;

public class Turret : MonoBehaviour
{
	private void Awake()
	{
		this.selfCollider = base.GetComponent<Collider>();
		base.Invoke("GetReady", Random.Range(this.fireRate * 0.5f, this.fireRate * 1.5f));
	}

	private void Update()
	{
		if (!Managers.Instance.head)
		{
			return;
		}
		if (GameManager.Instance.gameDone)
		{
			return;
		}
		Quaternion quaternion = Quaternion.LookRotation(Managers.Instance.head.position - this.turretHead.position);
		this.turretHead.transform.rotation = Quaternion.Lerp(this.turretHead.transform.rotation, quaternion, Time.deltaTime * 1.5f);
		RaycastHit raycastHit;
		if (!Physics.Raycast(this.projectilePos.position, this.projectilePos.forward, ref raycastHit, 50f, this.whatIsHittable))
		{
			return;
		}
		if (raycastHit.transform.name != "Head")
		{
			return;
		}
		if (this.readyToShoot)
		{
			this.Fire();
		}
	}

	private void Fire()
	{
		this.readyToShoot = false;
		Object.Instantiate<GameObject>(this.shootFx, this.projectilePos.position, Quaternion.identity);
		Quaternion quaternion = Quaternion.Euler((float)Random.Range(0, 360), (float)Random.Range(0, 360), (float)Random.Range(0, 360));
		GameObject gameObject = Object.Instantiate<GameObject>(this.bean, this.projectilePos.position, quaternion);
		gameObject.transform.localScale *= 2.5f;
		Rigidbody component = gameObject.GetComponent<Rigidbody>();
		component.AddForce(this.projectilePos.forward * 4f, 1);
		component.AddTorque(new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * 2f);
		Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), this.selfCollider, true);
		base.Invoke("GetReady", this.fireRate);
	}

	private void GetReady()
	{
		this.readyToShoot = true;
	}

	private void OnCollisionEnter(Collision other)
	{
		MonoBehaviour.print("other speed on turret: " + other.relativeVelocity.magnitude);
		if (other.relativeVelocity.magnitude > 6f)
		{
			Object.Instantiate<GameObject>(this.explosionFx, base.transform.position, this.explosionFx.transform.rotation);
			Object.Destroy(base.gameObject);
		}
	}

	public Transform turretHead;

	public Transform projectilePos;

	public GameObject bean;

	public LayerMask whatIsHittable;

	private bool readyToShoot;

	public GameObject shootFx;

	public GameObject explosionFx;

	private float fireRate = 5f;

	private Collider selfCollider;
}
