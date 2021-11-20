using UnityEngine;

public class BulletManager
{
    #region Singleton
    public static BulletManager Instance
    {
        get
        {
            if (instance == null)
                instance = new BulletManager();
            return instance;
        }
    }

    private static BulletManager instance;

    private BulletManager() { }
    #endregion

    Bullet bulletResource;
    public LayerMask hitableLayers = LayerMask.GetMask("Zombie", "ZombieHead");
    public LayerMask ZombieHeadLayer = LayerMask.NameToLayer("ZombieHead");

    public void Init()
    {
        bulletResource = Resources.Load<GameObject>("Prefabs/Bullet").GetComponent<Bullet>();
    }

    public void Shoot(Vector3 forward)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Bullet bullet = GameObject.Instantiate(bulletResource);
        bullet.Init(ray.origin + forward * 0.01f, ray.origin + ray.direction * 20);
        bullet.CollisionDetection(ray);
    }

}
