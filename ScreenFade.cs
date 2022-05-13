using System;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFade : MonoBehaviour
{
	private void Awake()
	{
		ScreenFade.Instance = this;
		this.image.CrossFadeAlpha(0f, 4f, false);
		this.image.enabled = true;
	}

	public void SetVisible()
	{
		this.image.CrossFadeAlpha(0f, 0f, true);
	}

	public void FadeBlack(float time)
	{
		this.image.CrossFadeAlpha(1f, time, true);
	}

	public void FadeHide(float time)
	{
		this.image.CrossFadeAlpha(1f, time, true);
	}

	public Image image;

	public static ScreenFade Instance;
}
