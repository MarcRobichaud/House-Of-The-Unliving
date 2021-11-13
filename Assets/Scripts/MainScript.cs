using UnityEngine;

public class MainScript : MonoBehaviour
{
    private void Awake()
    {
        LevelManager.Instance.Init();
        PlayerManager.Instance.Init();
        EnemyManager.Instance.Init();
    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        PlayerManager.Instance.Refresh();
    }
}