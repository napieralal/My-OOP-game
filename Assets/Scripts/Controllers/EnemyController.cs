using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //tez moze byc boss moze on bedzie po prostu jako 3 wrogow i musisz go pokozac 3 razy,
    //moze np. 2 wrogow to beda rece a pozniej cale cialo 
    //bedzie wielu ich
    private List<Enemy> enemies = new List<Enemy>();
    private Enemy currentEnemy;
    private int currentIndex = 0;
    
    private void Update()
    {
        HandleInput();
    }

    // Method to handle up and down arrow key input
    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            // Move to the previous enemy in the list
            if (currentIndex > 0)
            {
                currentIndex--;
                SetCurrentEnemy(enemies[currentIndex]);
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            // Move to the next enemy in the list
            if (currentIndex < enemies.Count - 1)
            {
                currentIndex++;
                SetCurrentEnemy(enemies[currentIndex]);
            }
        }
    }

    public void SetCurrentEnemy(Enemy enemy)
    {
        if (currentEnemy != null)
        {
            currentEnemy.healthSystem.SetCurrentBorderActive(false);
        }
        
        currentEnemy = enemy;
        currentIndex = enemies.IndexOf(currentEnemy);
        
        currentEnemy.healthSystem.SetCurrentBorderActive(true);
    }

    public Enemy GetCurrentEnemy()
    {
        return currentEnemy;
    }

    public void CreateEnemy(EnemySO enemySO, Transform enemySpawnPoint)
    {
        Transform enemyTransform = Instantiate(enemySO.enemyPrefab, enemySpawnPoint.position, Quaternion.identity);
        Enemy enemy = enemyTransform.GetComponent<Enemy>();
        enemies.Add(enemy);
        SetCurrentEnemy(enemy);
    }
    
    public void RemoveEnemy(Enemy enemy)
    {
        if (enemies.Contains(enemy))
        {
            int index = enemies.IndexOf(enemy);

            enemies.Remove(enemy);
            Debug.Log(enemy.gameObject.name + " has been removed from the enemy list.");
            
            if (index <= currentIndex)
            {
                currentIndex = Mathf.Max(0, currentIndex - 1);
            }

            if (enemies.Count > 0)
            {
                SetCurrentEnemy(enemies[currentIndex]);
            }
            else
            {
                currentEnemy = null;
                Debug.LogWarning("No enemies left in the list.");
            }
        }
        else
        {
            Debug.LogWarning("Attempted to remove an enemy that is not in the list.");
        }
    }
    
    public void DealDamageToCurrentEnemy(int damage, DamageTypeSO damageType)
    {
        if (currentEnemy != null)
        {
            Debug.Log("Dealing " + damage + " damage to current enemy: " + currentEnemy.gameObject.name);
            currentEnemy.TakeDamage(damage, damageType);
        }
        else
        {
            Debug.LogWarning("No current enemy to deal damage to.");
        }
    }
    
    //deal damage to enemieS

    public void DealDamageToPlayer()
    {
        foreach (Enemy enemy in enemies)
        {
            enemy.EnemyAttack();
        }
    }
}
