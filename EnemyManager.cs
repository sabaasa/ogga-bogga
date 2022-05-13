using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
	private void Start()
	{
		EnemyManager.Instance = this;
		this.enemies = new List<GameObject>();
		this.SpawnEnemy(this.normalEnemy, this.a0);
		this.SpawnEnemy(this.beanEnemies, this.a1);
		this.SpawnEnemy(this.flyingEnemies, this.a2);
		this.SpawnEnemy(this.turret, this.a3);
	}

	private void SpawnEnemy(GameObject enemyPrefab, int amount)
	{
		if (this.enemies.Count > 15)
		{
			return;
		}
		for (int i = 0; i < amount; i++)
		{
			Vector3 posOnDamageZone = Managers.Instance.GetPosOnDamageZone();
			Object.Instantiate<GameObject>(enemyPrefab, posOnDamageZone, Quaternion.identity);
		}
	}

	public void RemoveEnemy(GameObject e)
	{
		this.enemies.Remove(e);
		MonoBehaviour.print(string.Concat(new object[]
		{
			"removed: ",
			e,
			", new size: ",
			this.enemies.Count
		}));
		if (this.enemies.Count < 1)
		{
			MonoBehaviour.print("MAP DONE");
			GameManager.Instance.gameDone = true;
			if (!Player.Instance.IsDead())
			{
				GameManager.Instance.win = true;
				MonoBehaviour.print("player won");
			}
			GameManager.Instance.EndGame();
		}
	}

	public void AddEnemy(GameObject e)
	{
		MonoBehaviour.print("adding");
		this.enemies.Add(e);
		MonoBehaviour.print(string.Concat(new object[]
		{
			"added: ",
			e,
			", new size: ",
			this.enemies.Count
		}));
	}

	public GameObject spawnPos;

	public GameObject normalEnemy;

	public int a0;

	public GameObject beanEnemies;

	public int a1;

	public GameObject flyingEnemies;

	public int a2;

	public GameObject turret;

	public int a3;

	private List<GameObject> enemies;

	public static EnemyManager Instance;
}
