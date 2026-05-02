using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 3f;

    private Transform player;
    private EnemyDeath death;

    private void Awake()
    {
        death = GetComponent<EnemyDeath>();
        GameObject playerObj = GameObject.Find("VR Player");
        if (playerObj != null)
            player = playerObj.transform;
    }

    private void Update()
    {
        if (death.isDead || player == null) return;

        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0f;

        transform.position += direction * moveSpeed * Time.deltaTime;

        if (direction != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(direction);
    }
}