using System;
using UnityEngine;

public class LookCamera : MonoBehaviour
{
	private void Update()
	{
		Quaternion quaternion = Quaternion.LookRotation(this.target.position - base.transform.position);
		base.transform.rotation = Quaternion.Lerp(base.transform.rotation, quaternion, Time.deltaTime * 2f);
		if (!this.movement)
		{
			return;
		}
		base.transform.position = this.moveTarget.position + this.offset;
	}

	public Transform target;

	public bool movement;

	public Transform moveTarget;

	public Vector3 offset;
}
