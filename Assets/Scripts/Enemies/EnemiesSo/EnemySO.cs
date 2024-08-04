using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemySO : ScriptableObject
{
    public string enemyName;
    public int enemyHealth;
    public Sprite enemySprite;
    public Transform enemyPosition;

    public virtual void EnemyAttack()
    {
        
    }
}
