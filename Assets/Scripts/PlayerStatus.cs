using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;

    private List<Buff> activeBuffs = new List<Buff>();
    private List<PowerUp> activePowerUps = new List<PowerUp>();

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

    public void ApplyBuff(Buff buff)
    {
        activeBuffs.Add(buff);
        // Apply buff effects here
    }

    public void RemoveBuff(Buff buff)
    {
        activeBuffs.Remove(buff);
        // Remove buff effects here
    }

    public void ActivatePowerUp(PowerUp powerUp)
    {
        activePowerUps.Add(powerUp);
        // Apply power-up effects here
    }

    public void DeactivatePowerUp(PowerUp powerUp)
    {
        activePowerUps.Remove(powerUp);
        // Remove power-up effects here
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

[System.Serializable]
public class Buff
{
    public string name;
    // Add any properties or methods related to buffs here
}

[System.Serializable]
public class PowerUp
{
    public string name;
    // Add any properties or methods related to power-ups here
}
