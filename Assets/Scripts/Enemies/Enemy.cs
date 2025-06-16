using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable, IClickable
{
    public EnemySO enemyData;
    
    public HealthSystem baseHealthSystem;
    public HealthSystem healthSystem;

    private void Awake()
    {
        healthSystem = Instantiate(baseHealthSystem);
        healthSystem.healthChangeEvent.AddListener(OnHealthChanged);
        
        healthSystem.InitializeHealthBar(transform);
    }

    public void EnemyAttack()
    {
        Debug.Log(Player.Instance);
        Player.Instance.TakeDamage(enemyData.enemyDamage, enemyData.damageType);
    }

    public void TakeDamage(int damage, DamageTypeSO damageType)
    {
        if (IsVulnerableTo(damageType))
        {
            damage *= 2;
            Debug.Log(gameObject.name + " is vulnerable to " + damageType + ", damage doubled to " + damage);
        }
        else if (IsResistantTo(damageType))
        {
            damage = 0;
            Debug.Log(gameObject.name + " is resistant to " + damageType + ", no damage taken.");
        }
        Debug.Log(gameObject.name + " taking " + damage + " damage.");
        healthSystem.DecreaseHealth(damage, damageType.isMagic);
    }
    
    public bool IsVulnerableTo(DamageTypeSO damageType)
    {
        return enemyData.vulnerabilities != null && enemyData.vulnerabilities.Contains(damageType);
    }

    public bool IsResistantTo(DamageTypeSO damageType)
    {
        return enemyData.resistances != null && enemyData.resistances.Contains(damageType);
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
    
    public void OnClick()
    {
        if (GameController.Instance.enemyManager.GetCurrentEnemy() != this)
        {
            GameController.Instance.enemyManager.SetCurrentEnemy(this);
        }
    }
}