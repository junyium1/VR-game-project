using Oculus.Interaction.Feedback;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}

    public enum GameState {MainMenu, Fighting, RoundOver, Pause}
    public GameState currentState;

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
    public void StartRound()
    {
        currentState = GameState.Fighting;
    }

    public void EndRound(string winnerName)
    {
        currentState = GameState.RoundOver;
        Debug.Log("winner :" + winnerName);
    }
}
