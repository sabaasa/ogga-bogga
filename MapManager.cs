using System;
using UnityEngine;

public class MapManager : MonoBehaviour
{
	private void Awake()
	{
		this.yPos = this.center.y + this.size.y;
		this.Spawn(this.rock, 5);
		this.Spawn(this.tree, 3);
	}

	private void Spawn(GameObject go, int amount)
	{
		for (int i = 0; i < amount; i++)
		{
			Vector3 vector = new Vector3(this.center.x + Random.Range(-this.size.x / 2f, this.size.x / 2f), this.yPos, this.center.z + Random.Range(-this.size.z / 2f, this.size.z / 2f));
			Debug.DrawLine(vector, vector + Vector3.down * 5f, Color.black, 10f);
			RaycastHit raycastHit;
			if (Physics.Raycast(vector, Vector3.down, ref raycastHit, 50f, this.whatIsHittable))
			{
				Object.Instantiate<GameObject>(go, raycastHit.point, Quaternion.LookRotation(raycastHit.normal));
			}
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(this.center, this.size);
	}

	public LayerMask whatIsHittable;

	public GameObject rock;

	public GameObject tree;

	public Vector3 center;

	public Vector3 size;

	private float yPos;
}
