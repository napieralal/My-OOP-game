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

    public void SetCurrentEnemy(Enemy enemy)
    {
        currentEnemy = enemy;
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
            
            if (index > 0)
            {
                SetCurrentEnemy(enemies[index - 1]);
            }
            else if (enemies.Count > 0)
            {
                SetCurrentEnemy(enemies[0]);
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
    
    public void DealDamageToCurrentEnemy(int damage)
    {
        if (currentEnemy != null)
        {
            Debug.Log("Dealing " + damage + " damage to current enemy: " + currentEnemy.gameObject.name);
            currentEnemy.TakeDamage(damage);
        }
        else
        {
            Debug.LogWarning("No current enemy to deal damage to.");
        }
    }
}
