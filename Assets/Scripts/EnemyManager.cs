using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    public bool isSpawning = false;

    private float timeStarted;
    private float spawnInterval = 1f;
    private int enemyLeft;
    private int enemyToSpawn;
    private HashSet<Enemy> enemies = new HashSet<Enemy>();

    private bool SpawnTimerReady => Time.time > timeStarted + spawnInterval;

    private EnemyType GetRandomEnemyType => (EnemyType)UnityEngine.Random.Range(0, 3);
    private Vector3 GetRandomSpawner => LevelManager.Instance.CheckPointSpawners[UnityEngine.Random.Range(0, LevelManager.Instance.CheckPointSpawners.Count)].position;

    public void Init()
    {
        LevelManager.Instance.OnCheckPointReached.AddListener(SpawnEnemies);

        foreach (var enemy in enemies)
            enemy.Init();
    }

    public void Refresh()
    {
        isSpawning = enemyToSpawn > 0;

        if (isSpawning && SpawnTimerReady)
        {
            CreateEnemy(GetRandomEnemyType, GetRandomSpawner);
            timeStarted = Time.time;
            enemyToSpawn--;
        }
    }

    public void PhysicRefresh()
    {
        foreach (var enemy in enemies)
            enemy.Refresh();
    }

    public void CreateEnemy(EnemyType enemyType, Vector3 spawnPoint)
    {
        Enemy enemy = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/" + enemyType)).GetComponent<Enemy>();
        enemy.transform.position = spawnPoint;
        Debug.Log(spawnPoint);
        enemy.Init();

        enemies.Add(enemy);
    }

    public void SpawnEnemies()
    {
        enemyToSpawn = LevelManager.Instance.CheckPointEnemyNumber;
        enemyLeft = LevelManager.Instance.CheckPointEnemyNumber;
        timeStarted = Time.time;
    }

    public void Hit(Enemy enemy, bool headShot)
    {
        if (!headShot)
            enemy.stats.hp--;

        if (headShot || enemy.stats.hp <= 0)
            Die(enemy);
    }

    private void Die(Enemy enemy)
    {
        enemyLeft--;
        enemies.Remove(enemy);
        enemy.Die();

        if (enemyLeft <= 0)
        {
            LevelManager.Instance.ChangeCheckPoint();
            PlayerManager.Instance.MoveToNextCheckPoint();
        }
    }
}
