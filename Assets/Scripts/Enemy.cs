using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static EnemyData Data = new EnemyData();


    [SerializeField]
    private float maxHealth = Data.MaxHealth;
    private float currentHealth;
    
    
    public void Initialize(EnemyData enemyData)
    {
        Data = enemyData;
        currentHealth = Data.MaxHealth;
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