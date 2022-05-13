using System;
using UnityEngine;

public class MoveHole : MonoBehaviour
{
	private void Awake()
	{
		this.startPos = base.transform.position;
		base.Invoke("RemoveObject", 5f);
	}

	private void Update()
	{
		base.transform.position = Vector3.Lerp(base.transform.position, this.startPos + Vector3.down * 0.1f, Time.deltaTime * 0.3f);
	}

	private void RemoveObject()
	{
		Object.Destroy(base.transform.root.gameObject);
	}

	private Vector3 startPos;
}
