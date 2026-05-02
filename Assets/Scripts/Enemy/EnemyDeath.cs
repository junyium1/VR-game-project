using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    public float destroyDelay = 3f;
    public bool isDead = false;

    public void Die()
    {
        if (isDead) return;
        isDead = true;

        Destroy(gameObject, destroyDelay);
    }
}