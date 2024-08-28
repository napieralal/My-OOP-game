using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    void TakeDamage(int damage, DamageTypeSO damageType);
    bool IsVulnerableTo(DamageTypeSO damageType);
    bool IsResistantTo(DamageTypeSO damageType);
}
