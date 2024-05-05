using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public EnemyData Data;

    [SerializeField] private float currentHealth;

    [SerializeField] private GameObject projectilePrefab; // Reference to the projectile prefab
    [SerializeField] private int maxProjectiles = 3; // Maximum number of projectiles
    [SerializeField] private float fireRate = 1.0f; // Fire rate in shots per second
    private float _detectionRange;
    private EnemyType _enemyType;
    private float _fireTimer; // Timer to track firing cooldown

    private NavMeshAgent _navMeshAgent;

    private Transform _player;

    private ProjectileBehavior[] _projectiles; // Array to hold projectiles

    private void Start()
    {
        Initialize(Data);
    }

    private void Update()
    {
        var distanceToPlayer = Vector3.Distance(transform.position, _player.position);
        if (distanceToPlayer < _detectionRange)
            switch (_enemyType)
            {
                case EnemyType.Melee:
                    _navMeshAgent.SetDestination(_player.position);
                    break;
                case EnemyType.Range:
                    FireAtPlayer();
                    break;
            }

        // Update fire timer
        _fireTimer += Time.deltaTime;
    }

    public void Initialize(EnemyData enemyData)
    {
        Data = enemyData;
        _enemyType = Data.EnemyType;
        currentHealth = Data.MaxHealth;

        _player = GameObject.FindWithTag("Player").transform;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _detectionRange = Data.DetectionRange;
        fireRate = Data.FireRate;

        // Initialize projectiles array
        _projectiles = new ProjectileBehavior[maxProjectiles];
        for (var i = 0; i < maxProjectiles; i++)
        {
            var projectileObject = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            _projectiles[i] = projectileObject.GetComponent<ProjectileBehavior>();
            _projectiles[i].gameObject.SetActive(false); // Deactivate the projectile initially
        }
    }

    private void FireAtPlayer()
    {
        if (_fireTimer >= 1 / fireRate)
        {
            // Find an inactive projectile to fire
            var availableProjectile = GetAvailableProjectile();
            if (availableProjectile != null)
            {
                // Aim at the player
                transform.LookAt(_player);

                // Set projectile position and rotation
                availableProjectile.transform.position = transform.position;
                availableProjectile.transform.rotation = transform.rotation;

                // Initialize and fire the projectile
                availableProjectile.Initialize();
                availableProjectile.Fire();

                // Reset fire timer
                _fireTimer = 0f;
            }
        }
    }

    private ProjectileBehavior GetAvailableProjectile()
    {
        return _projectiles.FirstOrDefault(projectile => !projectile.gameObject.activeSelf);
    }

    public void TakeDamage(float damage)
    {
        if (!(currentHealth > 0)) return;

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