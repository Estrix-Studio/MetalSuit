using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    private int currentHealth;

    private Slider healthBar;

    // Start is called before the first frame update
    private void Start()
    {
        healthBar = GetComponent<Slider>();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void Damage(int dam)
    {
        Debug.Log("ouch2");
        currentHealth -= dam;
        healthBar.value = currentHealth;
        Debug.Log(healthBar.value);
    }

    public void Heal(int heal)
    {
        currentHealth += heal;
        healthBar.value = currentHealth;
        Debug.Log(healthBar.value);
    }

    public void SetMaxHealth(int max)
    {
        currentHealth = max;
        healthBar.maxValue = max;
        healthBar.value = currentHealth;
    }
}