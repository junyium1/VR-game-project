using UnityEngine;

public class HandCombat : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        EnemyKnockback enemy = other.GetComponent<EnemyKnockback>();
        if (enemy != null)
        {
            Vector3 hitDirection = (other.transform.position - transform.position).normalized;
            enemy.GetHit(hitDirection);
        }
    }
}