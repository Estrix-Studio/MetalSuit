using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    [SerializeField] private float maxHealth = 50f;
    private float currentHealth;

    public int damage = 5;
    public float knockbackForce = 15;
    public float knockbackDuration = 0.3f;
    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        // Handle enemy death
        Debug.Log("Enemy has been defeated!");
        // You may want to add more logic here, such as dropping items or triggering other events.
        Destroy(gameObject); // For simplicity, destroy the enemy GameObject upon death.
    }
}
