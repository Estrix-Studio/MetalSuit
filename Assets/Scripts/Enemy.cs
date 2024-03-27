using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public EnemyData Data;

    private Transform player;


    [SerializeField]
    private float currentHealth;

    private NavMeshAgent navMeshAgent;
    private float detectionRange;
    
    public void Initialize(EnemyData enemyData)
    {
        Data = enemyData;
        currentHealth = Data.MaxHealth;

        player = GameObject.FindWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        detectionRange = Data.DetectionRange;  
    }

    public void Start()
    {
        Initialize(Data);
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer < detectionRange) 
        {
            Debug.Log("Detec");
            navMeshAgent.SetDestination(player.position);
        }
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


