using System;
using UnityEngine;

public class FlyingBoi : Enemy
{
	private void Awake()
	{
		this.emission = this.smoke.emission;
		this.animator = base.GetComponent<Animator>();
		this.lr = new LineRenderer[]
		{
			this.gearPos[0].GetComponent<LineRenderer>(),
			this.gearPos[1].GetComponent<LineRenderer>()
		};
		this.sphereRand = new Vector3[2];
		this.audio = base.GetComponent<AudioSource>();
	}

	private void Update()
	{
		if (this.isRagdoll)
		{
			return;
		}
		if (!this.grappling)
		{
			return;
		}
		if (this.agent.enabled)
		{
			this.agent.enabled = false;
		}
		this.DrawLine();
		this.AirMovement();
		if (Vector3.Distance(base.transform.position, Managers.Instance.head.position) < 1f)
		{
			Player.Instance.Damage(30f, 1f);
			this.grappling = false;
			this.lr[0].positionCount = 0;
			this.lr[1].positionCount = 0;
			this.rb.isKinematic = false;
			Vector3 normalized = (Managers.Instance.damageZone.position - base.transform.position).normalized;
			this.rb.velocity = Vector3.zero;
			this.rb.AddForce(normalized * 5f, 1);
			this.rb.AddForce(Vector3.Cross(normalized, Vector3.up) * Random.Range(-3f, 3f), 1);
			this.rb.AddForce(Vector3.up * 7f, 1);
		}
	}

	private void DrawLine()
	{
		for (int i = 0; i < this.lr.Length; i++)
		{
			this.lr[i].SetPosition(0, this.gearPos[i].position);
			this.lr[i].SetPosition(1, Managers.Instance.head.position + this.sphereRand[i]);
		}
	}

	private void AirMovement()
	{
		Vector3 normalized = (Managers.Instance.neck.position - base.transform.position).normalized;
		float num = 500f;
		this.rb.AddForce(normalized * num * Time.deltaTime);
		this.rb.AddForce(Vector3.up * num * 1.2f * Time.deltaTime);
		this.rb.AddForce(Vector3.Cross(normalized, Vector3.up) * num * 0.2f * Time.deltaTime);
	}

	protected override void SlowUpdate()
	{
		if (!this.agent.isOnNavMesh)
		{
			return;
		}
		if (GameManager.Instance.gameDone)
		{
			return;
		}
		if (this.agent.remainingDistance < 0.4f)
		{
			if (this.lr == null)
			{
				return;
			}
			this.animator.Play("Grappling");
			this.grappling = true;
			this.lr[0].positionCount = 2;
			this.lr[1].positionCount = 2;
			this.rb.useGravity = true;
			this.rb.isKinematic = false;
			this.rb.AddForce(Vector3.up * 3.5f, 1);
			this.sphereRand[0] = Random.onUnitSphere * 0.7f;
			this.sphereRand[1] = Random.onUnitSphere * 0.7f;
			this.emission.enabled = true;
			this.trail.emitting = true;
			this.audio.Play();
		}
	}

	private void LateUpdate()
	{
		if (!this.grappling)
		{
			return;
		}
		if (this.torso)
		{
			this.torso.LookAt(Managers.Instance.head);
		}
	}

	private void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("Other") && this.grappling)
		{
			this.rb.AddForce(base.transform.right * 5f, 1);
		}
		if (this.grappling)
		{
			return;
		}
		if (other.gameObject.layer != LayerMask.NameToLayer("Ground"))
		{
			return;
		}
		this.agent.enabled = true;
		this.rb.isKinematic = true;
		base.FindPosition();
		this.animator.Play("Running");
		if (this.smoke == null)
		{
			return;
		}
		this.emission.enabled = false;
		this.trail.emitting = false;
	}

	private Animator animator;

	public bool grappling;

	private LineRenderer[] lr;

	public Transform[] gearPos;

	public ParticleSystem smoke;

	private ParticleSystem.EmissionModule emission;

	public TrailRenderer trail;

	private AudioSource audio;

	private Vector3[] sphereRand;
}
