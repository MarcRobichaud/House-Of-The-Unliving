using UnityEngine;

public class MainScript : MonoBehaviour
{
    private void Start()
    {
        LevelManager.Instance.Init();
        PlayerManager.Instance.Init();
        EnemyManager.Instance.Init();
        InputManager.Instance.Init();
        BulletManager.Instance.Init();
        UIManager.Instance.Init();
    }

    private void FixedUpdate()
    {
        EnemyManager.Instance.PhysicRefresh();
    }

    private void Update()
    {
        PlayerManager.Instance.Refresh();
        EnemyManager.Instance.Refresh();
        InputManager.Instance.Refresh();
        UIManager.Instance.Refresh();
    }
}