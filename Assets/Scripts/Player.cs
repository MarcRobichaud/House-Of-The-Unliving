using System;
using UnityEngine;

enum PlayerState
{
    Moving,
    Shooting
}

public class Player : MonoBehaviour
{
    private float timeStarted;
    private float length = 20;
    private PlayerState playerState;
    private Vector3 movement;

    public PlayerStats stats;

    public void Init()
    {
        stats.ammo = PlayerManager.MaxAmmo;
        transform.position = LevelManager.Instance.SpawnPoint;
        StartMoving();
    }

    public void Refresh()
    {
        switch (playerState)
        {
            case PlayerState.Moving:
                Move();
                break;
            case PlayerState.Shooting:
                Shooting();
                break;
            default:
                break;
        }
    }

    public void StartMoving()
    {
        movement = LevelManager.Instance.CheckPointPosition;
        movement -= transform.position;
        movement = transform.right * movement.x + transform.forward * movement.z;
        playerState = PlayerState.Moving;
    }

    private void Move()
    {
        transform.position += movement.normalized * stats.movementSpeed * Time.deltaTime;
        if (transform.position.x >= LevelManager.Instance.CheckPointPosition.x)
        {
            LevelManager.Instance.CheckPointReached();
            StartShooting();
        }
    }

    private void StartShooting()
    {
        timeStarted = Time.time;
        playerState = PlayerState.Shooting;
    }

    private void Shooting()
    {
        if (InputManager.Instance.InputInfos.shoot && stats.ammo > 0)
        {
            stats.ammo--;
            BulletManager.Instance.Shoot(transform.forward);
        }
    }
}
