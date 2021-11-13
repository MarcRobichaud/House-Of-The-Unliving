using System;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    NormalZombie,
    TankZombie,
    BreakDancerZombie
}

public class EnemyManager
{
    #region Singleton
    public static EnemyManager Instance
    {
        get
        {
            if (instance == null)
                instance = new EnemyManager();
            return instance;
        }
    }

    private static EnemyManager instance;

    private EnemyManager() { }
    #endregion

    private HashSet<Enemy> enemies = new HashSet<Enemy>();

    public void Init()
    {
        foreach (var enemy in enemies)
            enemy.Init();
    }

    public void Refresh()
    {
        foreach (var enemy in enemies)
            enemy.Refresh();
    }

    public void CreateEnemy(EnemyType enemyType, Vector3 spawnPoint)
    {
        Enemy enemy = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/" + enemyType)).GetComponent<Enemy>();
        enemy.transform.position = spawnPoint;

        enemies.Add(enemy);
    }
}
