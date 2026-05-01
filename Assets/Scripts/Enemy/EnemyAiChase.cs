using UnityEngine;
using UnityEngine.AI;

public class EnemyAiChase : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public float knockbackForce = 10f;
    public float destroyDelay = 3f;

    private Rigidbody rb;
    private bool isDead = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("VR Player").transform;
    }

    private void Update()
    {
        if (!isDead && player)
        {
            agent.SetDestination(player.position);
        }
    }

    public void GetHit(Vector3 hitDirection)
    {
        if (isDead) return;
        isDead = true;

        Destroy(agent);
        rb.isKinematic = false;
        rb.constraints = RigidbodyConstraints.None;
        rb.angularDamping = 0f;
        rb.AddForce(hitDirection * knockbackForce + Vector3.up * knockbackForce * 0.5f, ForceMode.Impulse);
        rb.AddTorque(Random.insideUnitSphere * knockbackForce, ForceMode.Impulse);

        Destroy(gameObject, destroyDelay);
    }
}