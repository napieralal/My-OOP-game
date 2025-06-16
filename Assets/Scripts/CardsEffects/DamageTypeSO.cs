using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageType
{
    Normal,
    Ice,
    Fire,
}

[CreateAssetMenu(fileName = "Damage Type", menuName = "ScriptableObjects/Damage Type/Damage Type")]
public class DamageTypeSO : ScriptableObject
{
    public DamageType damageType;
    public bool isMagic;

}
