using Oculus.Interaction.Feedback;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}

    public enum GameState {MainMenu, Fighting, RoundOver, Pause}
    public GameState currentState;

    [Header("Param de l'arène")]
    public float timeLimit = 120f;
    private float currentTime;

    public int enemiesToSpawn = 5;
    private int enemiesDefeated = 0;

    [Header("Spawning")]
    public GameObject enemyPrefab;
    public Transform[] spawnpoints;
    private string winnerName;


    void Start()
    {
        StartRound();
    }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        if (currentState == GameState.Fighting)
        {
            currentTime = Time.deltaTime;

            if (currentTime <= 0)
            {

            }
        }
    }



    public void StartRound()
    {
        currentState = GameState.Fighting;
        currentTime = timeLimit;
        enemiesDefeated = 0;
        Debug.Log("Temps imparti : " + timeLimit + "s");
        SpawnEnemies();
    }

    public void SpawnEnemies()
    {
        if (spawnpoints.Length == 0 || enemyPrefab == null) return;

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Transform sp = spawnpoints[Random.Range(0, spawnpoints.Length)];
            Instantiate(enemyPrefab, sp.position, Quaternion.identity);
        }
    }

    public void enemyKilled()
    {
        if (currentState != GameState.Fighting) return;

        enemiesDefeated++;
        Debug.Log("Ennemi battu (" + enemiesDefeated + "/" + enemiesToSpawn + ")");

        if (enemiesDefeated >= enemiesToSpawn)
        {
            currentState = GameState.RoundOver;
            Debug.Log("K.O. " + winnerName);
        }
    }






    public void EndRound(string winnerName)
    {
        currentState = GameState.RoundOver;
        Debug.Log("winner :" + winnerName);
    }
}
