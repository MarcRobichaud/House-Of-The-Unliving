using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{

    public Vector3 CheckPoint => checkPoints[currentCheckPoint].position;
    public Transform SpawnPoint;

    [SerializeField]
    private List<Transform> checkPoints;

    int currentCheckPoint = 0;

    public void Init()
    {

    }

    public void CheckPointReached()
    {
        currentCheckPoint++;
    }
}
