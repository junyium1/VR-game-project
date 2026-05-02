using UnityEngine;

public class EnemyKnockback : MonoBehaviour
{
    public float knockbackForce = 10f;

    private Rigidbody rb;
    private EnemyDeath death;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        death = GetComponent<EnemyDeath>();
    }

    public void GetHit(Vector3 hitDirection)
    {
        if (death.isDead) return;

        rb.isKinematic = false;
        rb.constraints = RigidbodyConstraints.None;
        rb.angularDamping = 0f;
        rb.AddForce(hitDirection * knockbackForce + Vector3.up * knockbackForce * 0.5f, ForceMode.Impulse);
        rb.AddTorque(Random.insideUnitSphere * knockbackForce, ForceMode.Impulse);

        death.Die();
    }
}