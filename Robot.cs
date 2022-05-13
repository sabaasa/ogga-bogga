using System;
using UnityEngine;

public class Robot : MonoBehaviour
{
	private void Awake()
	{
		this.rigidEnemy = base.GetComponent<RigidEnemy>();
	}

	private void FixedUpdate()
	{
		Vector3 normalized = (this.target.position - this.rigidEnemy.root.position).normalized;
		this.rigidEnemy.MoveBody(normalized);
		this.rigidEnemy.RotateBody(normalized);
	}

	private RigidEnemy rigidEnemy;

	public Transform target;
}
