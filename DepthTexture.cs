using System;
using UnityEngine;

[ExecuteInEditMode]
public class DepthTexture : MonoBehaviour
{
	private void Start()
	{
		this.cam = base.GetComponent<Camera>();
		this.cam.depthTextureMode = 1;
	}

	private Camera cam;
}
