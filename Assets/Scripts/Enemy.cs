using System;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Serializable]
    public struct EnemyStat
    {
        public float movementSpeed;
        public int hp;
    }

    public EnemyStat stats;

    private NavMeshAgent agent;

    public void Init()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = true;
        agent.speed = stats.movementSpeed;
    }

    public void Refresh()
    {
        agent.destination = PlayerManager.Instance.PlayerPosition;
    }

}
