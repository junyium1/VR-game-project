using Oculus.Interaction.Feedback;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}

    public enum GameState {MainMenu, Fighting, RoundOver, Pause}
    public GameState currentState;

    [Header("Param de l'arene")]
    public float timeLimit = 120f;
    private float currentTime;

    public int enemiesToSpawn = 5;
    private int enemiesDefeated = 0;
    
    public GameObject enemyPrefab;
    private string winnerName;
    
    public Transform playerTransform;
    public float spawnRadius = 8f;


    
    void Start()
    {
        currentState = GameState.MainMenu;
    }

    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Arena Alpha");
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
            currentTime -= Time.deltaTime;

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
        if (enemyPrefab == null) return;

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Vector3 spawnPos = GetRandomSpawnAroundPlayer();
            Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        }
    }


    private Vector3 GetRandomSpawnAroundPlayer()
    {
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        float x = Mathf.Cos(angle) * spawnRadius;
        float z = Mathf.Sin(angle) * spawnRadius;
        return new Vector3(
            playerTransform.position.x + x,
            playerTransform.position.y,
            playerTransform.position.z + z
        );
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


    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    
    
    public void EndRound(string reason)
    {
        currentState = GameState.RoundOver;
        GameObject canvas = GameObject.FindWithTag("GameOverCanvas");
        if (canvas != null) canvas.SetActive(true);
    }
}
