using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Gère la vie d'un ennemi. Attacher sur le GameObject racine de l'ennemi.
/// Assigner un EnemyData dans l'inspecteur pour configurer les stats.
/// </summary>
[RequireComponent(typeof(EnemyRagdoll))]
public class EnemyHealth : MonoBehaviour
{
    [Header("Configuration")]
    [Tooltip("Données de l'ennemi (ScriptableObject)")]
    public EnemyData data;

    [Header("Événements")]
    public UnityEvent<float> OnDamaged;   // param : dégâts reçus
    public UnityEvent OnDeath;

    public float CurrentHealth { get; private set; }
    public bool IsDead { get; private set; }

    void Start()
    {
        if (data == null)
        {
            Debug.LogError($"[EnemyHealth] Aucun EnemyData assigné sur {gameObject.name} !", this);
            return;
        }
        CurrentHealth = data.maxHealth;
    }

    /// <summary>Inflige des dégâts à l'ennemi.</summary>
    public void TakeDamage(float amount)
    {
        if (IsDead || data == null) return;

        CurrentHealth = Mathf.Max(0f, CurrentHealth - amount);
        OnDamaged.Invoke(amount);

        if (CurrentHealth <= 0f)
            Kill();
    }

    private void Kill()
    {
        if (IsDead) return;
        IsDead = true;

        OnDeath.Invoke();

        EnemyRagdoll ragdoll = GetComponent<EnemyRagdoll>();
        if (ragdoll != null)
            ragdoll.EnableRagdoll();

        if (data.destroyDelay > 0f)
            Destroy(gameObject, data.destroyDelay);
    }
}
