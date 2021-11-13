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
    
    private static PlayerManager instance;

    private PlayerManager() { }
    #endregion
    private Player player;

    public Vector3 PlayerPosition => player.transform.position;

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
