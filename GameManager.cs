using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	private void Awake()
	{
		GameManager.Instance = this;
	}

	private void Start()
	{
	}

	public void EndGame()
	{
		if (this.win)
		{
			this.PlayerWin();
			MonoBehaviour.print("Player win. Show UI.");
			return;
		}
		MonoBehaviour.print("Player lost. show UI / restart");
		base.Invoke("ShowDeathScreen", 2.1f);
		ScreenFade.Instance.FadeBlack(2f);
	}

	private void PlayerWin()
	{
		base.Invoke("EndWin", 1.5f);
	}

	private void ShowDeathScreen()
	{
		SceneManager.LoadScene("DeathScreen");
	}

	private void EndWin()
	{
		ScreenFade.Instance.FadeBlack(2f);
		base.Invoke("NextLevel", 2.1f);
	}

	private void NextLevel()
	{
		int buildIndex = SceneManager.GetActiveScene().buildIndex;
		string text = "Island" + (buildIndex + 1);
		if (buildIndex + 3 > SceneManager.sceneCountInBuildSettings)
		{
			MonoBehaviour.print("Game Completely done");
			SceneManager.LoadScene("MainMenu");
			return;
		}
		SceneManager.LoadScene(text);
	}

	public bool gameDone;

	public bool win;

	public static GameManager Instance;
}
