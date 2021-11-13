using UnityEngine;

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

    public Vector3 NextCheckPoint => level.CheckPoint;
    public Vector3 SpawnPoint => level.SpawnPoint.position;
    public void CheckPointReached() => level.CheckPointReached();

    public void Init()
    {
        GenerateLevel();
    }

    public void Refresh()
    {

    }

    private void GenerateLevel()
    {
        level = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Level1")).GetComponent<Level>();
    }
}