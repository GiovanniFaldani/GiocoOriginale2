using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 10;
    private int currentHealth;

    [Header("Events")]
    public UnityEvent onTakeDamage;
    public UnityEvent onDie;

    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;

    private void OnEnable()
    {
        currentHealth = maxHealth;
    }

    /// Applica danno al nemico.
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Max(currentHealth, 0);
        onTakeDamage?.Invoke();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    /// Ripristina la salute al massimo.
    public void RestoreFullHealth()
    {
        currentHealth = maxHealth;
    }

    /// Gestione della morte del nemico.    
    private void Die()
    {
        onDie?.Invoke();

        // Rimuovi o disattiva il nemico (torna nel pool)
        ObjectPooler.ReturnToPool(gameObject);
    }
}
