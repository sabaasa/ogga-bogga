using System;
using UnityEngine;

public class BounceEnemy : MonoBehaviour, IBounce
{
	public void Bounce()
	{
		base.GetComponent<Enemy>().GrabEnemy();
		base.GetComponent<Rigidbody>().AddForce(Vector3.up * 15f, 1);
	}
}
