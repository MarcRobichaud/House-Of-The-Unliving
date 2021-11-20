using UnityEngine;

public class MainScript : MonoBehaviour
{
    private void Awake()
    {
        LevelManager.Instance.Init();
        PlayerManager.Instance.Init();
        EnemyManager.Instance.Init();
        InputManager.Instance.Init();
        BulletManager.Instance.Init();
        UIManager.Instance.Init();
    }

    private void Update()
    {
        PlayerManager.Instance.Refresh();
        EnemyManager.Instance.Refresh();
        InputManager.Instance.Refresh();
        UIManager.Instance.Refresh();
    }
}