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
    [SerializeField] public Texture2D cardImage;
    [SerializeField] public int cardCost;
    [SerializeField] public string cardDesc;
    [SerializeField] public string cardAction;
    [SerializeField] public int cardSpecial;
    [SerializeField] public List<CardEffectSO> cardEffects = new List<CardEffectSO>();
    
    void PlayCard()
    {
        // resolve effects
        //go to discard pile (if card says its destroyed then destroy it)
    }

    void Attack()
    {
        
    }

    void Defense()
    {
        
    }

    void Move()
    {
        
    }

    void DrawCard()
    {
        
    }

    void Heal()
    {
        
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
