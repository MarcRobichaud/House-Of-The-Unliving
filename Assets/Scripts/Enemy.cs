using System;
using System.Collections;
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

    private Animator anim;
    private NavMeshAgent agent;
    private Vector3 destination = Vector3.zero;

    private bool attacking = false;

    public void Init()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        agent.enabled = true;
        agent.speed = stats.movementSpeed;
        SetDestination();
        
    }

    public void Refresh()
    {
        if (destination != PlayerManager.Instance.PlayerPosition)
            SetDestination();

        if (!attacking)
        {
            float dist = (agent.destination - agent.transform.position).magnitude;
            attacking = dist <= agent.stoppingDistance;

            if (attacking)
                anim.SetTrigger("Attacking");
        }
    }

    private void SetDestination()
    {
        destination = PlayerManager.Instance.PlayerPosition;
        agent.SetDestination(destination);
    }

    public void HitPlayer()
    {
        Debug.Log("Hit");
        PlayerManager.Instance.Hit();
    }

    public void Die()
    {
        agent.isStopped = true;
        anim.SetTrigger("Dead");
        StartCoroutine(DieAnimation());
    }

    public IEnumerator DieAnimation()
    {
        yield return null;
        while (anim.GetCurrentAnimatorStateInfo(0).IsName("Zombie Death") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.99)
        {

            yield return null;
        }
        Destroy(gameObject);
    }
}
