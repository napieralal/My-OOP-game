using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
    public static Player Instance { get; private set; }
    
    public PlayerSO playerData;
    public HealthSystem healthSystem;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        
        healthSystem.healthChangeEvent.AddListener(OnHealthChanged);
        
        healthSystem.InitializeHealthBar(transform);
    }

    void Update()
    {
        
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
        return
            playerData.vulnerabilities != null && playerData.vulnerabilities.Contains(damageType);
    }

    public bool IsResistantTo(DamageTypeSO damageType)
    {
        return
            playerData.resistances != null && playerData.resistances.Contains(damageType);
    }

    public void RecoverHealth(int amount)
    {
        healthSystem.IncreaseHealth(amount);
    }

    public void ShieldAdd(int amount)
    {
        healthSystem.IncreaseShield(amount);
    }
    
    private void OnHealthChanged(int currentHealth)
    {
        Debug.Log(gameObject.name + " has " + currentHealth + " health remaining.");
        if (currentHealth <= 0)
        {
            Defeat();
        }
    }
    
    private void Defeat()
    {
        Debug.Log(gameObject.name + " has been defeated.");
        //GameController.Instance.enemyManager.RemoveEnemy(this);
        //Destroy(gameObject);
    }
    
    private void OnDestroy()
    {
        healthSystem.healthChangeEvent.RemoveListener(OnHealthChanged);
    }
}
