using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shield Effect", menuName = "ScriptableObjects/Card Effects/ShieldEffect")]
public class ShieldEffectSO : CardEffectSO
{
    [SerializeField]
    private int shieldAmount;
    
    public override void ApplyEffect()
    {
        if (GameController.Instance != null)
        {
            Debug.Log("Shield added: " + shieldAmount);
            Player.Instance.ShieldAdd(shieldAmount);
        }
        else
        {
            Debug.LogError("GameController.Instance is null.");
        }
    }
}