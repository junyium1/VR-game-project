using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    private bool isDead = false;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void GetHit(Vector3 knockbackDirection, float force)
    {
        if (!isDead) return;
        isDead = true;
        rb.isKinematic = false;
        rb.AddForce(knockbackDirection * force, ForceMode.Impulse);
        rb.AddTorque(Random.insideUnitSphere * force, ForceMode.Impulse);

        if (GameManager.Instance != null)
        {
            GameManager.Instance.enemyKilled();
        }

        Destroy(gameObject, 3f);
    }
}
