using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    private bool isDead = false;
    private Rigidbody rb;
    public float moveSpeed = 2f;
    private Transform player;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void Update()
    {
        if (!isDead || player == null) return;
        
        Vector3 dir = (player.position - transform.position).normalized;
        transform.position += dir * moveSpeed * Time.deltaTime;
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
    }
    public void GetHit(Vector3 knockbackDirection, float force)
    {
        if (isDead) return;
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

    private void OnCollisionEnter(Collision other)
    {
        if (isDead) return;
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerStats>()?.TakeDamage();
            Destroy(gameObject);
        }
    }
    
    
}
