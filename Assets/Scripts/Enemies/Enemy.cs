using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    public EnemySO enemyData;
    public HealthSystem baseHealthSystem;
    public HealthSystem healthSystem;

    private void Start()
    {
        healthSystem = Instantiate(baseHealthSystem);
        healthSystem.healthChangeEvent.AddListener(OnHealthChanged);
        
        healthSystem.InitializeHealthBar(transform);
    }

    public void EnemyAttack()
    {
        // Logika ataku wroga
    }

    public void TakeDamage(int damage)
    {
        Debug.Log(gameObject.name + " taking " + damage + " damage.");
        healthSystem.DecreaseHealth(damage);
    }
    
    private void OnHealthChanged(int currentHealth)
    {
        Debug.Log(gameObject.name + " has " + currentHealth + " health remaining.");
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log(gameObject.name + " has died.");
        GameController.Instance.enemyManager.RemoveEnemy(this);
        Destroy(gameObject);
    }
    
    private void OnDestroy()
    {
        healthSystem.healthChangeEvent.RemoveListener(OnHealthChanged);
    }
}