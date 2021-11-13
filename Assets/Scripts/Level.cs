using System;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [Serializable]
    public struct CheckPoint
    {
        public Transform transform;
        public int numbersOfEnemies;
        public List<Transform> spawners;
    }

    public Vector3 NextCheckPoint => checkPoints[currentCheckPoint].transform.position;
    public Transform SpawnPoint;

    [SerializeField]
    private List<CheckPoint> checkPoints;

    int currentCheckPoint = 0;

    public void Init()
    {

    }

    public void CheckPointReached()
    {
        if (currentCheckPoint < checkPoints.Count)
            currentCheckPoint++;
    }
}
