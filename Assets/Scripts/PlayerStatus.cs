using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;
    private PlayerMovement PlayerMovement;
    private HealthBarController healthBarController;
    private FinishSuitController finishSuitController;
    public void Initialize()
    {
        PlayerMovement = GetComponent<PlayerMovement>();
        healthBarController = FindAnyObjectByType<HealthBarController>();
        healthBarController.SetMaxHealth(maxHealth);
        finishSuitController = FindAnyObjectByType<FinishSuitController>();
        // Initialization code here, if needed
    }
    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage, Vector3 damageSourcePosition, float knockbackForce, float knockbackDuration)
    {
        Debug.Log("OUCH");
        if (currentHealth > 0)
        {
            currentHealth -= damage;
            healthBarController.Damage(damage);

            if(finishSuitController != null) 
            {
                finishSuitController.SuitDestruction(currentHealth, maxHealth);
            }

            if (currentHealth <= 0)
            {
                Die();
            }
            PlayerMovement.TakeDamage(damageSourcePosition,  knockbackForce,  knockbackDuration);
        }
    }

    public void Heal(int amount)
    {
        if (currentHealth > 0)
        {
            healthBarController.Heal(amount);
            currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
            Debug.Log("Heal");
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
        Vector3 collisionPosition = collision.contacts[0].point;
        int recivedDamage =0;
        float recivedForce =0;
        float recivedDuration = 0;

        if (PlayerMovement.GetIsDashing())
        {
            EnemyStatus enemyStatus = collision.gameObject.GetComponent<EnemyStatus>();
            if (enemyStatus != null)
            {
                // Damage the enemy only once during the dash
                enemyStatus.TakeDamage(10f); // Adjust the damage amount as needed
                Debug.Log("Hit!!");
            }
        }
        else if(collision.transform.tag == "Enemy")
        {
           
            EnemyStatus enemyStatus = collision.gameObject.GetComponent<EnemyStatus>();

            if(enemyStatus != null) 
            {
                recivedDamage = enemyStatus.damage;
                recivedForce =enemyStatus.knockbackForce;
                recivedDuration =enemyStatus.knockbackDuration;
            }
            TakeDamage(recivedDamage, collisionPosition, recivedForce, recivedDuration);
        }
        else if (collision.transform.tag == "Obstacle")
        {
            Obstacle obstacle = collision.transform.GetComponent<Obstacle>();
            if (obstacle != null)
            {
                recivedDamage = obstacle.damage;
                recivedForce = obstacle.knockbackForce;
                recivedDuration = obstacle.knockbackDuration;
            }

            TakeDamage(recivedDamage, collisionPosition, recivedForce, recivedDuration);
        }

        else if( collision.transform.tag == "Repair")
        {
            Heal(5);
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Repair")
        {
            Heal(5);
            Destroy(other.gameObject);
        }
    }

}


