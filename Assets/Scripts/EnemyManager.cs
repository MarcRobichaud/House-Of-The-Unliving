using System;

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

    public void Init()
    { 
    }

    private void CreateEnemy()
    { }
}
