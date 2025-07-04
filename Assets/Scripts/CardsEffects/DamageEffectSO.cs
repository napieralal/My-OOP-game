using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Damage Effect", menuName = "ScriptableObjects/Card Effects/DamageEffect")]
public class DamageEffectSO : CardEffectSO
{
    [SerializeField]
    private int damageAmount;
    //plus jakies inne modyfikatory
    
    [SerializeField]
    private DamageTypeSO damageType;
    
    public override void ApplyEffect()
    {
        if (GameController.Instance != null)
        {
            Debug.Log("Damage dealt: " + damageAmount);
            GameController.Instance.enemyManager.DealDamageToCurrentEnemy(damageAmount, damageType);
        }
        else
        {
            Debug.LogError("GameController.Instance is null.");
        }
    }
}
