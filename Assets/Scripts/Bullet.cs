using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Material laserMat;
    
    LineRenderer lineRenderer;
    float laserWidth = 0.025f;
    float laserTime = 0.5f;


    public void Init(Vector3 origin, Vector3 direction)
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = laserMat;
        lineRenderer.widthMultiplier = laserWidth;
        lineRenderer.positionCount = 2;
        Vector3[] positions = { origin, direction };
        lineRenderer.SetPositions(positions);
        StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(laserTime);
        Destroy(gameObject);
    }

    public void CollisionDetection(Ray ray)
    {
        RaycastHit[] hits = Physics.RaycastAll(ray, 20, BulletManager.Instance.hitableLayers);
        bool headShot = false ;
        Enemy enemyHit = null;

        if (hits.Length > 0)
        {
            foreach (var hit in hits)
            {
                if (hit.transform.gameObject.layer == BulletManager.Instance.ZombieHeadLayer)
                    headShot = true;

                if (enemyHit == null)
                    enemyHit = hit.transform.GetComponentInParent<Enemy>();
            }
            EnemyManager.Instance.Hit(enemyHit, headShot);
        }
    }
}
