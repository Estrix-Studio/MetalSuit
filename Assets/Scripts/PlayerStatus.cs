using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;


    public void Initialize()
    {

        // Initialization code here, if needed
    }
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

    public void Heal(float amount)
    {
        if (currentHealth > 0)
        {
            currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        }
    }


    private void Die()
    {
        // Handle player death
        Debug.Log("Player has died!");
        // You may want to add more logic here, such as respawning the player or triggering a game over.
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (gameObject.GetComponent<PlayerMovement>().GetIsDashing())
        {
            EnemyStatus enemyStatus = collision.gameObject.GetComponent<EnemyStatus>();
            if (enemyStatus != null)
            {
                // Damage the enemy only once during the dash
                enemyStatus.TakeDamage(10f); // Adjust the damage amount as needed
                Debug.Log("Hit!!");
            }
        }
    }
}


