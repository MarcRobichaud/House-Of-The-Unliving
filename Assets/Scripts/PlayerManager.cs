using System;
using UnityEngine;

[Serializable]
public struct PlayerStats
{
    public float movementSpeed;
    public int hp;
    public int ammo;
}

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
    public PlayerStats Stats => player.stats;
    public void MoveToNextCheckPoint() => player.StartWaiting();
    public void Hit() => player.Hit();
    public void AddAmmo() => player.AddAmmo();
    public const int MaxAmmo = 5;
    public const int MaxLife = 5;

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
