using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnRadius = 20f;
    public float minInterval = 3f;
    public float maxInterval = 7f;

    void Start()
    {
        SpawnEnemy();
        ScheduleNextSpawn();
    }

    void ScheduleNextSpawn()
    {
        float interval = Random.Range(minInterval, maxInterval);
        Invoke(nameof(SpawnAndSchedule), interval);
    }

    void SpawnAndSchedule()
    {
        SpawnEnemy();
        ScheduleNextSpawn();
    }

    void SpawnEnemy()
    {
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        Vector3 spawnPos = transform.position + new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle)) * spawnRadius;

        if (Physics.Raycast(spawnPos + Vector3.up * 10f, Vector3.down, out RaycastHit hit, 20f))
            spawnPos = hit.point + Vector3.up * 1f;

        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }
}