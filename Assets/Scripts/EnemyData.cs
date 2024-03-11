public struct EnemyData
{
    public float MaxHealth;
    public int Damage;
    public float KnockbackForce;
    public float KnockbackDuration;
    public float CurrentHealth;

    public EnemyData(float maxHealth, int damage, float knockbackForce, float knockbackDuration)
    {
        MaxHealth = maxHealth;
        Damage = damage;
        KnockbackForce = knockbackForce;
        KnockbackDuration = knockbackDuration;
        CurrentHealth = maxHealth;
    }
}