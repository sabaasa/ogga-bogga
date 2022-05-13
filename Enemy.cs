using System;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
	private void Start()
	{
		EnemyManager.Instance.AddEnemy(base.gameObject);
		this.startPos = base.transform.position;
		this.rb = base.GetComponent<Rigidbody>();
		this.agent = base.GetComponent<NavMeshAgent>();
		base.Invoke("FindPosition", this.startDelay);
		base.InvokeRepeating("SlowUpdate", this.startDelay + 0.4f, 0.4f);
	}

	protected void FindPosition()
	{
		if (!this.agent.isOnNavMesh)
		{
			return;
		}
		this.agent.SetDestination(Managers.Instance.GetPosOnDamageZone());
	}

	protected virtual void SlowUpdate()
	{
		if (!this.agent.isOnNavMesh)
		{
			return;
		}
		if (this.agent.remainingDistance < 0.4f)
		{
			this.FindPosition();
		}
		Debug.DrawLine(base.transform.position, this.agent.destination, Color.black, 0.4f);
	}

	public void Kill()
	{
	}

	public void GrabEnemy()
	{
		if (this.isRagdoll)
		{
			return;
		}
		EnemyManager.Instance.RemoveEnemy(base.gameObject);
		this.agent.enabled = false;
		this.rb.isKinematic = false;
		this.rb.useGravity = true;
		Object.Destroy(base.transform.GetChild(0).gameObject);
		GameObject gameObject = Object.Instantiate<GameObject>(this.ragdoll, base.transform.position, base.transform.rotation);
		gameObject.GetComponent<Joint>().connectedBody = this.rb;
		this.ragdollObject = gameObject;
		this.isRagdoll = true;
	}

	private void OnDestroy()
	{
		if (!GameManager.Instance)
		{
			return;
		}
		EnemyManager.Instance.RemoveEnemy(base.gameObject);
	}

	public void ReleaseEnemy()
	{
	}

	public GameObject ragdoll;

	protected NavMeshAgent agent;

	private Transform target;

	protected Rigidbody rb;

	[HideInInspector]
	public bool isRagdoll;

	private Vector3 startPos;

	[HideInInspector]
	public GameObject ragdollObject;

	public Transform head;

	public Transform torso;

	public float startDelay;
}
