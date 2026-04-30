using UnityEngine;
using UnityEngine.AI;

public class EnemyAiChase : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (player)
        {
            agent.SetDestination(player.position);
        }
    }
}