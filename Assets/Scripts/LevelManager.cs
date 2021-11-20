using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager
{
    #region Singleton
    public static LevelManager Instance
    {
        get
        {
            if (instance == null)
                instance = new LevelManager();
            return instance;
        }
    }

    private static LevelManager instance;

    private LevelManager() { }
    #endregion

    private Level level;

    public UnityEvent OnCheckPointReached => level.OnCheckPointReached;
    public Vector3 SpawnPoint => level.SpawnPoint.position;
    public Vector3 CheckPointPosition => level.NextCheckPointPosition;
    public int CheckPointEnemyNumber => level.NextCheckPointEnemyNumber;
    public List<Transform> CheckPointSpawners => level.NextCheckPointSpawners;
    public void CheckPointReached() => level.CheckPointReached();
    public void ChangeCheckPoint() => level.ChangeCheckPoint();

    public void Init()
    {
        GenerateLevel();
    }

    private void GenerateLevel()
    {
        level = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Level1")).GetComponent<Level>();
    }
}