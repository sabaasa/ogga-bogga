using System;
using TMPro;
using UnityEngine;

public class LevelUI : MonoBehaviour
{
	private void Awake()
	{
		this.text = base.GetComponent<TextMeshProUGUI>();
		base.Invoke("StartFade", 2f);
		this.desiredScale = base.transform.localScale * 1.3f;
	}

	private void StartFade()
	{
		this.text.CrossFadeAlpha(0f, 3f, false);
		base.Invoke("DestroySelf", 3f);
	}

	private void Update()
	{
		base.transform.localScale = Vector3.Lerp(base.transform.localScale, this.desiredScale, Time.deltaTime * 0.1f);
	}

	private void DestroySelf()
	{
		Object.Destroy(base.gameObject);
	}

	private TextMeshProUGUI text;

	private Vector3 desiredScale;
}
