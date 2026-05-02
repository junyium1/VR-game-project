using UnityEngine;

public class PlayerHitbox : MonoBehaviour
{
    private PlayerStats playerStats;
    private float damageCooldown = 1f;
    private float lastHitTime = -999f;

    private void Awake()
    {
        playerStats = GetComponentInParent<PlayerStats>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("PlayerHitbox touché par : " + other.name);
        if (Time.time - lastHitTime < damageCooldown) return;
        if (other.GetComponent<EnemyDeath>() == null)
        {
            Debug.Log("Pas un ennemi, ignoré");
            return;
        }

        lastHitTime = Time.time;
        Debug.Log("DÉGATS PRIS !");
        playerStats.TakeDamage();
    }
}