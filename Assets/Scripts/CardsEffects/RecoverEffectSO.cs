using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recover Effect", menuName = "ScriptableObjects/Card Effects/RecoverEffect")]
public class RecoverEffectSO : CardEffectSO
{
    [SerializeField]
    private int recoverAmount;
    
    public override void ApplyEffect()
    {
        if (GameController.Instance != null)
        {
            Debug.Log("Health recovered: " + recoverAmount);
            Player.Instance.RecoverHealth(recoverAmount);
        }
        else
        {
            Debug.LogError("GameController.Instance is null.");
        }
    }
}