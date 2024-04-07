using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private GameUIStateMachine gameUIStateMachine;
    private int currentHealth;
    private FinishSuitController finishSuitController;
    private HealthBarController healthBarController;
    private PlayerMovement PlayerMovement;


    private void Start()
    {
        currentHealth = maxHealth;
        finishSuitController = GetComponent<FinishSuitController>();
        gameUIStateMachine = GameObject.FindObjectOfType<GameUIStateMachine>();

    }

    private void OnCollisionEnter(Collision collision)
    {
        var collisionPosition = collision.contacts[0].point;
        var receivedDamage = 0;
        float receivedDuration = 0;
        float receivedForce = 0;

        if (PlayerMovement.GetIsDashing())
        {
            var enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy == null) return;

            // Damage the enemy only once during the dash
            enemy.TakeDamage(10f); // Adjust the damage amount as needed
            Debug.Log("Hit!!");
        }
        else if (collision.transform.CompareTag("Enemy"))
        {
            var enemyStatus = collision.gameObject.GetComponent<Enemy>();

            if (enemyStatus != null)
            {
                receivedDamage = enemyStatus.GetData().Damage;
                receivedForce = enemyStatus.GetData().KnockbackForce;
                receivedDuration = enemyStatus.GetData().KnockbackDuration;
            }

            TakeDamage(receivedDamage, collisionPosition, receivedForce, receivedDuration);
        }
        else if (collision.transform.CompareTag("Obstacle"))
        {
            var obstacle = collision.transform.GetComponent<Obstacle>();
            if (obstacle == null)
                obstacle = collision.transform.parent.transform.GetComponent<Obstacle>();
            if (obstacle != null)
            {
                receivedDamage = obstacle.damage;
                receivedForce = obstacle.knockbackForce;
                receivedDuration = obstacle.knockbackDuration;
            }

            TakeDamage(receivedDamage, collisionPosition, receivedForce, receivedDuration);
        }

        else if (collision.transform.CompareTag("Repair"))
        {
            Heal(5);
            Destroy(collision.gameObject);
        }
        

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Repair"))
        {
            Heal(5);
            Destroy(other.gameObject);
        }
        else if (other.transform.CompareTag("FinishLine"))
        {
            gameUIStateMachine.ChangeToWinScreen();
        }
    }

    public void Initialize()
    {
        PlayerMovement = GetComponent<PlayerMovement>();
        healthBarController = FindAnyObjectByType<HealthBarController>();
        healthBarController.SetMaxHealth(maxHealth);
        finishSuitController = FindAnyObjectByType<FinishSuitController>();
        // Initialization code here, if needed
    }

    public void TakeDamage(int damage, Vector3 damageSourcePosition, float knockbackForce, float knockbackDuration)
    {
        if (currentHealth <= 0) return;
        currentHealth -= damage;
        healthBarController.Damage(damage);

        if (finishSuitController != null)
            finishSuitController.SuitDestruction(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
           
        PlayerMovement.TakeDamage(damageSourcePosition, knockbackForce, knockbackDuration);
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
        gameUIStateMachine.ChangeToLoseScreen();
        // You may want to add more logic here, such as respawning the player or triggering a game over.
    }
}