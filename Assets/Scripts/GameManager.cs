using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}
    public GameObject gameOverCanvas;


    public enum GameState {MainMenu, Fighting, RoundOver, Pause}
    public GameState currentState;

    [Header("Param de l'arene")]
    public float timeLimit = 120f;
    private float currentTime;

    private string winnerName;

    void Start()
    {
    }

    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Arena Alpha");
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
    }
    

    public void EndRound(string reason)
    {
        currentState = GameState.RoundOver;

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            Destroy(enemy);

        if (gameOverCanvas != null)
        {
            Camera cam = Camera.main;
            gameOverCanvas.transform.position = cam.transform.position + cam.transform.forward * 1.5f;
            gameOverCanvas.transform.rotation = Quaternion.LookRotation(cam.transform.forward);
            gameOverCanvas.SetActive(true);
        }
    }
    
    public void RestartGame()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
