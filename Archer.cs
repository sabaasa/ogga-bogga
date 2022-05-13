using System;
using UnityEngine;

public class Archer : Enemy
{
	private void Awake()
	{
		this.audioSource = base.GetComponent<AudioSource>();
		this.animator = base.GetComponent<Animator>();
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
			this.animator.Play("Attacking");
		}
	}

	private void Bean()
	{
		if (!this.beanPos)
		{
			return;
		}
		Rigidbody component = Object.Instantiate<GameObject>(this.bean, this.beanPos.position, Quaternion.identity).GetComponent<Rigidbody>();
		Vector3 vector = Managers.Instance.head.position - this.beanPos.position;
		component.AddForce(vector * 0.6f, 1);
		component.AddTorque(new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * 2f);
		this.audioSource.Play();
		this.beansThrown++;
		this.readyToThrowBean = false;
		base.Invoke("BeanCooldown", Random.Range(0.4f, 0.75f));
		if (this.beansThrown > 0)
		{
			base.FindPosition();
			this.beansThrown = 0;
			this.animator.Play("Running");
		}
	}

	private void Update()
	{
		if (this.throwBean && this.readyToThrowBean && this.beansThrown < 2)
		{
			this.Bean();
		}
	}

	private void LateUpdate()
	{
		if (this.torso)
		{
			this.torso.LookAt(Managers.Instance.head);
		}
	}

	private void BeanCooldown()
	{
		this.readyToThrowBean = true;
	}

	private Animator animator;

	public bool throwBean;

	public GameObject bean;

	public Transform beanPos;

	private bool throwing;

	private int beansThrown;

	private bool readyToThrowBean = true;

	private AudioSource audioSource;
}
