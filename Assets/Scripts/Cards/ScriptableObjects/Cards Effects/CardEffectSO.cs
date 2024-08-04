using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Card Effect",menuName ="Card Effect")]
public abstract class CardEffectSO : ScriptableObject
{
    public string effectName;
    public int effectId; //maybe I need this

    public virtual void CardEffect(EnemySO enemy)
    {
        
    }
}