using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardEffectSO : ScriptableObject
{
    private int effectPriority;
    //visual
    //sound
    //duration
    public virtual void ApplyEffect() {}
}
