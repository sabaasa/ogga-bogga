using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathUI : MonoBehaviour
{
	public void MainMenu()
	{
		SceneManager.LoadScene("MainMenu");
	}

	public void Restart()
	{
		SceneManager.LoadScene(Status.Instance.lastLevel);
	}

	public void FirstMap()
	{
		SceneManager.LoadScene("Island1");
	}
}
