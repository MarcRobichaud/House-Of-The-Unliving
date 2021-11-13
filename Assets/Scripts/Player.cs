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
    private float length = 2;
    private Rigidbody rb;
    private PlayerState playerState;
    private Vector3 destination;

    [Serializable]
    public struct PlayerStats
    {
        public float movementSpeed;
        public int hp;
        public int ammo;
    }

    public PlayerStats stats;

    public void Init()
    {
        rb = GetComponent<Rigidbody>();
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

    private void StartMoving()
    {
        destination = LevelManager.Instance.NextCheckPoint;
        destination = transform.right * destination.x + transform.forward * destination.z;
        rb.velocity = (destination - transform.position).normalized * stats.movementSpeed;
        playerState = PlayerState.Moving;
    }

    private void Move()
    {
        if (transform.position.x >= LevelManager.Instance.NextCheckPoint.x)
        {
            LevelManager.Instance.CheckPointReached();
            rb.velocity = new Vector3();
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
        if (Time.time > timeStarted + length)
            StartMoving();
    }
}
