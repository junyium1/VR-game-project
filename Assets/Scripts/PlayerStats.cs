using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [Header("Health")]
    public int maxLives = 2;
    public int currentLives;
    public GameObject[] hearts;

    private bool isDead = false;

    private void Start()
    {
        currentLives = maxLives;
        UpdateHeartsUI();
    }

    public void TakeDamage()
    {
        if (isDead) return;

        currentLives--;
        UpdateHeartsUI();

        if (currentLives <= 0)
        {
            isDead = true;
            Die();
        }
    }

    private void UpdateHeartsUI()
    {
        for (int i = 0; i < hearts.Length; i++)
            hearts[i].SetActive(i < currentLives);
    }

    private void Die()
    {
        GameManager.Instance?.EndRound("Player died");
    }
}