using UnityEngine;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "Enemy", order = 1)]
public class EnemyData : ScriptableObject
{
    public EnemyType EnemyType;
    public float DetectionRange;
    public float MaxHealth;
    public int Damage;
    public float KnockbackForce;
    public float KnockbackDuration;
    public float CurrentHealth;

    //public EnemyData(EnemyType enemyType, float detectionRange ,float maxHealth, int damage, float knockbackForce, float knockbackDuration)
    //{
    //    EnemyType = enemyType;
    //    DetectionRange = detectionRange;
    //    MaxHealth = maxHealth;
    //    Damage = damage;
    //    KnockbackForce = knockbackForce;
    //    KnockbackDuration = knockbackDuration;
    //    CurrentHealth = maxHealth;
    //}
}

public enum EnemyType
{
    Melee,
    Range
}