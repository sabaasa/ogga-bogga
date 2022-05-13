using System;
using UnityEngine;

public class Status : MonoBehaviour
{
	private void Awake()
	{
		Status.Instance = this;
		Object.DontDestroyOnLoad(base.gameObject);
	}

	public string lastLevel;

	public static Status Instance;
}
