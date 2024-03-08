using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Slider = UnityEngine.UI.Slider;

public class HealthBarController : MonoBehaviour
{
    Slider healthBar;

    int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        healthBar = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
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
