using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class HPBar : MonoBehaviour
{
	private void Awake()
	{
		HPBar.Instance = this;
		this.desiredScale = this.current.localScale;
		this.vignette = this.pp.GetSetting<Vignette>();
		this.vignette.intensity.value = 0f;
	}

	public void UpdateHp(float hp, float maxHp, float vignetteIntensity)
	{
		float num = hp / maxHp;
		this.vignetteFallbackIntensity = (1f - num) * 0.7f;
		if (vignetteIntensity > 0f)
		{
			this.vignette.intensity.value = 0.65f + (1f - num) * 0.2f;
			base.transform.localScale *= 1.2f;
		}
		this.desiredScale = new Vector3(num, 1f, 1f);
	}

	private void Update()
	{
		this.current.localScale = Vector3.Lerp(this.current.localScale, this.desiredScale, Time.deltaTime * 5f);
		base.transform.localScale = Vector3.Lerp(base.transform.localScale, Vector3.one, Time.deltaTime * 5f);
		this.vignette.intensity.value = Mathf.Lerp(this.vignette.intensity.value, this.vignetteFallbackIntensity, Time.deltaTime * 3f);
	}

	private void OnApplicationQuit()
	{
		this.vignette.intensity.value = 0f;
	}

	public Transform current;

	public Transform max;

	public static HPBar Instance;

	private Vector3 desiredScale;

	public PostProcessProfile pp;

	private Vignette vignette;

	private float vignetteFallbackIntensity;
}
