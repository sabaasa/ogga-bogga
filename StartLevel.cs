using System;
using UnityEngine;

public class StartLevel : MonoBehaviour
{
	private void Awake()
	{
		this.originalPos = new Vector3[this.objectsToMove.Length];
		for (int i = 0; i < this.objectsToMove.Length; i++)
		{
			this.originalPos[i] = this.objectsToMove[i].position;
			this.objectsToMove[i].position += this.offset;
		}
		base.Invoke("GetReady", 1f);
		base.Invoke("StopReady", 5f);
	}

	private void StopReady()
	{
		this.ready = false;
	}

	private void GetReady()
	{
		this.ready = true;
	}

	public void Update()
	{
		if (!this.ready)
		{
			return;
		}
		for (int i = 0; i < this.objectsToMove.Length; i++)
		{
			this.objectsToMove[i].position = Vector3.Lerp(this.objectsToMove[i].position, this.originalPos[i], Time.deltaTime * 2f);
		}
	}

	public Transform[] objectsToMove;

	public Vector3[] originalPos;

	public Vector3 offset;

	private bool ready;
}
