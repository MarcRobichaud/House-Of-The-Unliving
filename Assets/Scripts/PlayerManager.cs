using System;
using UnityEngine;

public class PlayerManager
{
    #region Singleton
    public static PlayerManager Instance
    {
        get
        {
            if (instance == null)
                instance = new PlayerManager();
            return instance;
        }
    }
    #endregion
    private static PlayerManager instance;

    private PlayerManager() { }

    private Player player;

    public void Init()
    {
        CreatePlayer();
        player.Init();
    }

    public void Refresh()
    {
        player.Refresh();
    }

    public void CreatePlayer()
    {
        player = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Player")).GetComponent<Player>();
    }
}
