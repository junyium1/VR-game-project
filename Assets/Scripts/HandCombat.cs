using UnityEngine;

public class HandCombat : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        EnemyAiChase enemy = other.GetComponent<EnemyAiChase>();
        if (enemy != null)
        {
            Vector3 hitDirection = (other.transform.position - transform.position).normalized;
            enemy.GetHit(hitDirection);
        }
    }
}
