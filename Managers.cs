using System;
using UnityEngine;

public class Managers : MonoBehaviour
{
	private void Awake()
	{
		Managers.Instance = this;
		this.neck = this.head.GetChild(0);
		Bounds bounds = this.damageZone.GetComponent<BoxCollider>().bounds;
		this.zoneLength = bounds.extents.x;
		this.zoneWidth = bounds.extents.z;
		Debug.DrawLine(this.damageZone.position, this.damageZone.position + Vector3.right * this.zoneLength, Color.black, 10f);
	}

	public Vector3 GetPosOnDamageZone()
	{
		float num = this.damageZone.position.x + Random.Range(-this.zoneLength, this.zoneLength);
		float num2 = this.damageZone.position.z + Random.Range(-this.zoneWidth, this.zoneWidth);
		return new Vector3(num, this.damageZone.position.y, num2);
	}

	public Transform damageZone;

	private float zoneLength;

	private float zoneWidth;

	public Transform head;

	public Transform neck;

	public static Managers Instance;
}
