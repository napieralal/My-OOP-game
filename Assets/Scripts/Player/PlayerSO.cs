using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Player", menuName = "ScriptableObjects/Player/Player")]
public class PlayerSO : ScriptableObject
{
    public string playerName;
    [SerializeField] public Transform playerPrefab;
    public Sprite playerSprite;
    public List<DamageTypeSO> vulnerabilities;
    public List<DamageTypeSO> resistances;
}
