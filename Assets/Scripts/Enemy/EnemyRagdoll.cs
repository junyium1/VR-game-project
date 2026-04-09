using UnityEngine;

/// <summary>
/// Gère le passage en ragdoll d'un ennemi à sa mort.
///
/// Setup requis sur le prefab ennemi :
///   - Un Animator sur le root (ou un enfant)
///   - Des Rigidbody + Collider sur chaque os (générés via Window > Animation > Avatar > Create Ragdoll)
///   - Tous les Rigidbody des os doivent être des enfants du root
///
/// Au Start, le ragdoll est désactivé (kinematic = true, colliders disabled).
/// EnableRagdoll() l'active et désactive l'Animator.
/// </summary>
public class EnemyRagdoll : MonoBehaviour
{
    [Header("Références (optionnel — auto-détecté si vide)")]
    [Tooltip("L'Animator à désactiver lors du ragdoll. Auto-détecté si non assigné.")]
    public Animator animatorToDisable;

    private Rigidbody[] ragdollBodies;
    private Collider[]  ragdollColliders;

    void Awake()
    {
        if (animatorToDisable == null)
            animatorToDisable = GetComponentInChildren<Animator>();

        // Récupère tous les Rigidbody enfants SAUF celui du root (locomotion)
        ragdollBodies    = GetComponentsInChildren<Rigidbody>();
        ragdollColliders = GetComponentsInChildren<Collider>();

        SetRagdollActive(false);
    }

    /// <summary>Active le ragdoll. Appelé automatiquement par EnemyHealth à la mort.</summary>
    public void EnableRagdoll()
    {
        if (animatorToDisable != null)
            animatorToDisable.enabled = false;

        SetRagdollActive(true);
    }

    private void SetRagdollActive(bool active)
    {
        foreach (Rigidbody rb in ragdollBodies)
        {
            rb.isKinematic = !active;
        }

        foreach (Collider col in ragdollColliders)
        {
            col.enabled = active;
        }
    }
}
