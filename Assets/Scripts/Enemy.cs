using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public EnemyData Data;

    private Transform player;

    [SerializeField] private float currentHealth;

    private NavMeshAgent navMeshAgent;
    private float detectionRange;
    private EnemyType enemyType;

    [SerializeField] private GameObject projectilePrefab; // Reference to the projectile prefab
    [SerializeField] private int maxProjectiles = 3; // Maximum number of projectiles
    [SerializeField] private float fireRate = 1.0f; // Fire rate in shots per second
    private float fireTimer = 0f; // Timer to track firing cooldown

    private ProjectileBehavior[] projectiles; // Array to hold projectiles

    public void Initialize(EnemyData enemyData)
    {
        Data = enemyData;
        enemyType = Data.EnemyType;
        currentHealth = Data.MaxHealth;

        player = GameObject.FindWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        detectionRange = Data.DetectionRange;
        fireRate = Data.FireRate;

        // Initialize projectiles array
        projectiles = new ProjectileBehavior[maxProjectiles];
        for (int i = 0; i < maxProjectiles; i++)
        {
            GameObject projectileObject = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectiles[i] = projectileObject.GetComponent<ProjectileBehavior>();
            projectiles[i].gameObject.SetActive(false); // Deactivate the projectile initially
        }
    }

    private void Start()
    {
        Initialize(Data);
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer < detectionRange)
        {
            switch (enemyType)
            {
                case EnemyType.Melee:
                    navMeshAgent.SetDestination(player.position);
                    break;
                case EnemyType.Range:
                    FireAtPlayer();
                    break;
            }
        }

        // Update fire timer
        fireTimer += Time.deltaTime;
    }

    private void FireAtPlayer()
    {
        if (fireTimer >= 1 / fireRate)
        {
            // Find an inactive projectile to fire
            ProjectileBehavior availableProjectile = GetAvailableProjectile();
            if (availableProjectile != null)
            {
                // Aim at the player
                transform.LookAt(player);

                // Set projectile position and rotation
                availableProjectile.transform.position = transform.position;
                availableProjectile.transform.rotation = transform.rotation;

                // Initialize and fire the projectile
                availableProjectile.Initialize();
                availableProjectile.Fire();

                // Reset fire timer
                fireTimer = 0f;
            }
        }
    }

    private ProjectileBehavior GetAvailableProjectile()
    {
        foreach (var projectile in projectiles)
        {
            if (!projectile.gameObject.activeSelf)
            {
                return projectile;
            }
        }
        return null;
    }

    public void TakeDamage(float damage)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                SoundManager.instance.PlayerSound(Sound.EnemyDie);
                Die();
            }
            else
            {
                SoundManager.instance.PlayerSound(Sound.EnemyHit);
            }
        }
    }

    private void Die()
    {
        Debug.Log("Enemy has been defeated!");
        Destroy(gameObject);
    }

    public EnemyData GetData()
    {
        return Data;
    }
}
