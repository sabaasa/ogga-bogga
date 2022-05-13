using System;
using UnityEngine;

public class Tree : MonoBehaviour
{
	private void Awake()
	{
		Color color;
		color..ctor(Random.Range(this.c1.r, this.c2.r), Random.Range(this.c1.g, this.c2.g), Random.Range(this.c1.b, this.c2.b));
		base.GetComponent<Renderer>().materials[1].color = color;
		base.transform.rotation = Quaternion.Euler(0f, (float)Random.Range(0, 360), 0f);
		base.transform.localScale *= Random.Range(0.8f, 1.2f);
	}

	public Color c1;

	public Color c2;
}
