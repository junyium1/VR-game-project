using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [Header("Health")]
    public int maxLives = 2;
    public int currentLives;
    public GameObject[] hearts;
    

    private void Start()
    {
        currentLives = maxLives;
        UpdateHeartsUI();
    }

    private void Update()
    {
    }

    public void TakeDamage()
    {
        currentLives--;
        UpdateHeartsUI();
        if (currentLives <= 0)
        {
            Die();
        }
    }

    private void UpdateHeartsUI()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].SetActive(i < currentLives);
        }
    }

    private void Die()
    {
        GameManager.Instance?.EndRound("Player died");
    }
}
