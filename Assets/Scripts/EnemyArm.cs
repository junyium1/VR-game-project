using UnityEngine;

public class EnemyArm : MonoBehaviour
{
    public float spinSpeed = 180f;
    private float damageCooldown = 1f;
    private float lastHitTime = -999f;

    void Update()
    {
        transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        if (Time.time - lastHitTime < damageCooldown) return;

        lastHitTime = Time.time;
        other.gameObject.GetComponent<PlayerStats>()?.TakeDamage();
    }
}
