using Oculus.VoiceSDK.UX;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [Header("Health")]
    public int maxLives = 2;
    public int currentLives;
    public GameObject[] hearts;

    [Header("Endurance")]
    public float maxStamina = 100f;
    public float currentStamina;
    public float staminaRegenRate = 15f;
    public Slider staminaBar;

    private void Start()
    {
        currentLives = maxLives;
        currentStamina = maxStamina;
        UpdateHeartsUI();
    }

    private void Update()
    {
        if (currentStamina < maxStamina)
        {
            currentStamina += staminaRegenRate * Time.deltaTime;
            if (currentStamina > maxStamina) currentStamina = maxStamina;
            if (staminaBar != null) staminaBar.value = currentStamina / maxStamina;
        }
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
