using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Level : MonoBehaviour
{
    [Serializable]
    public struct CheckPoint
    {
        public Transform transform;
        public int numbersOfEnemies;
        public List<Transform> spawners;
    }

    public Vector3 NextCheckPointPosition => checkPoints[currentCheckPoint].transform.position;
    public int NextCheckPointEnemyNumber => checkPoints[currentCheckPoint].numbersOfEnemies;
    public List<Transform> NextCheckPointSpawners => checkPoints[currentCheckPoint].spawners;

    public Transform SpawnPoint;
    public UnityEvent OnCheckPointReached;

    [SerializeField]
    private List<CheckPoint> checkPoints;
    private int currentCheckPoint = 0;

    public void Init()
    {

    }

    public void CheckPointReached()
    {
        OnCheckPointReached?.Invoke();
    }

    public void ChangeCheckPoint()
    {
        if (currentCheckPoint < checkPoints.Count - 1)
            currentCheckPoint++;
    }
}
