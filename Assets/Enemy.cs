using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform[] path;  // Các điểm trên đường đi
    public int health = 100;  // Thêm thuộc tính health
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

    // Hàm này được gọi khi kẻ thù bị tấn công
    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            Destroy(gameObject);  // Hủy đối tượng khi health về 0
        }
    }
}
