using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Card", menuName = "ScriptableObjects/Card")] 
public class CardSO : ScriptableObject
{
    [SerializeField] public string cardName;
    [SerializeField] public Transform cardPrefab;
    [SerializeField] public Sprite cardImage;
    [SerializeField] public int cardCost;
    [SerializeField] public string cardDesc;
    [SerializeField] public string cardAction;
    [SerializeField] public int cardSpecial;
    [SerializeField] public List<CardEffectSO> cardEffects = new List<CardEffectSO>();
    

    void Move()
    {
        
    }

    void DrawCard()
    {
        // w sensie zmien ta karte na inna
    }

    void GetItem()
    {
        
    }

    void DiscardCard()
    {
        
    }

    void GetGold()
    {
        
    }

    void ChooseOne()
    {
        
    }
}
