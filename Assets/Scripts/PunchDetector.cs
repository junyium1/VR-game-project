using UnityEngine;

public class PunchDetector : MonoBehaviour
{
    public float minPunchSpeed = 1.5f;
    public float knockbackForce = 8f;
    private Vector3 lastPosition;
    private float handSpeed;
    
    
    private void Update()
    {
        handSpeed = (transform.position - lastPosition).magnitude / Time.deltaTime;
        lastPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy == null) return;

        if (handSpeed >= minPunchSpeed)
        {
            Vector3 direction = (other.transform.position - transform.position).normalized;
            enemy.GetHit(direction, knockbackForce);
        }
    }
}
