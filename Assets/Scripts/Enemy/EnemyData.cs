using UnityEngine;

/// <summary>
/// ScriptableObject configurant les stats d'un type d'ennemi.
/// Créer via : clic droit > Create > Enemy > Enemy Data
/// </summary>
[CreateAssetMenu(fileName = "NewEnemyData", menuName = "Enemy/Enemy Data")]
public class EnemyData : ScriptableObject
{
    [Header("Vie")]
    [Min(1)]
    public float maxHealth = 100f;

    [Header("Mort")]
    [Tooltip("Durée avant destruction du GameObject après la mort (0 = jamais détruit)")]
    public float destroyDelay = 5f;
}
