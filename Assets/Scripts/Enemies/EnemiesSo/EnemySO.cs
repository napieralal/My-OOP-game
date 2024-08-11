using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "ScriptableObjects/Enemies/Enemy")]
public class EnemySO : ScriptableObject
{
    public string enemyName;
    [SerializeField] public Transform enemyPrefab;
    public Sprite enemySprite;
}
