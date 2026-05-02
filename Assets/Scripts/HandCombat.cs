using UnityEngine;

public class HandCombat : MonoBehaviour
{
    public float knockbackForce = 10f;
    public float minPunchAngle = 90f;

    private void OnTriggerEnter(Collider other)
    {
        EnemyDeath death = other.GetComponent<EnemyDeath>();
        if (death == null || death.isDead) return;

        Vector3 toEnemy = (other.transform.position - transform.position).normalized;
        Vector3 handForward = transform.forward;
        float angle = Vector3.Angle(handForward, toEnemy);

        Debug.Log("Angle main-ennemi : " + angle);

        if (angle > minPunchAngle)
        {
            Debug.Log("Ennemi pas devant la main, ignoré");
            return;
        }

        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.constraints = RigidbodyConstraints.None;
            rb.AddForce(toEnemy * knockbackForce + Vector3.up * knockbackForce * 0.5f, ForceMode.Impulse);
            rb.AddTorque(Random.insideUnitSphere * knockbackForce, ForceMode.Impulse);
        }

        Debug.Log("Ennemi frappé !");
        death.Die();
    }
}