using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform[] path;  // Các ?i?m trên ???ng ?i
    public int health = 100;  // Thêm thu?c tính health
    private NavMeshAgent agent;
    private int currentPathIndex = 0;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (path.Length > 0)
        {
            agent.SetDestination(path[currentPathIndex].position);
        }
    }

    void Update()
    {
        if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            currentPathIndex = (currentPathIndex + 1) % path.Length;
            agent.SetDestination(path[currentPathIndex].position);
        }
    }

    // Hàm này ???c g?i khi k? thù b? t?n công
    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            Destroy(gameObject);  // H?y ??i t??ng khi health v? 0
        }
    }
}
