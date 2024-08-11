using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    public List<CardSO> deck = new List<CardSO>();
    public List<CardSO> discardPile = new List<CardSO>();
    public List<CardSO> playerHand = new List<CardSO>();
    
    public Transform deckContainer;
    public Transform discardPileContainer;
    public List<Transform> handContainer;
    
    public GameObject cardBackPrefab;
    public GameObject emptyPilePrefab;
    public GameObject defaultEmptyPilePrefab;

    private void Start()
    {
        ShuffleDeck();
        UpdateDeckDisplay();
        UpdateDiscardPileDisplay();
    }
    
    public void CreateCard(CardSO cardSO, Transform spawnPoint)
    {
        Transform cardTransform = Instantiate(cardSO.cardPrefab, spawnPoint.position, spawnPoint.rotation, spawnPoint);
        Card card = cardTransform.GetComponent<Card>();
        card.InitializeCard(cardSO);
        cardTransform.localPosition = Vector3.zero;
    }
    
    public void ShuffleDeck()
    {
        for (int i = 0; i < deck.Count; i++)
        {
            CardSO temp = deck[i];
            int randomIndex = Random.Range(i, deck.Count);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
    }
    
    public void DrawCards(int count)
    {
        for (int i = 0; i < count; i++)
        {
            if (deck.Count == 0)
            {
                RefillDeckFromDiscard();
            }
            if (deck.Count == 0) return;

            CardSO drawnCard = deck[0];
            deck.RemoveAt(0);
            playerHand.Add(drawnCard);
        }
        UpdateDeckDisplay();
    }
    
    public void DiscardHand()
    {
        discardPile.AddRange(playerHand);
        playerHand.Clear();
        UpdateDiscardPileDisplay();
        DrawCards(5);
        UpdateHandDisplay();
    }

    public void DiscardCard(CardSO card)
    {
        playerHand.Remove(card);
        discardPile.Add(card);
        UpdateDiscardPileDisplay();
    }

    public void RefillDeckFromDiscard()
    {
        deck.AddRange(discardPile);
        discardPile.Clear();
        ShuffleDeck();
        UpdateDeckDisplay();
        UpdateDiscardPileDisplay();
    }
    
    public void SpawnHand(List<Transform> spawnPoints)
    {
        for (int i = 0; i < playerHand.Count; i++)
        {
            CreateCard(playerHand[i], spawnPoints[i]);
        }
    }
    
    public void UpdateHandDisplay()
    {
        foreach (Transform container in handContainer)
        {
            foreach (Transform child in container)
            {
                Destroy(child.gameObject);
            }
        }
        
        SpawnHand(handContainer);
    }
    
    private void UpdateDeckDisplay()
    {
        foreach (Transform child in deckContainer)
        {
            Destroy(child.gameObject);
        }

        if (deck.Count > 0)
        {
            Instantiate(cardBackPrefab, deckContainer.position, deckContainer.rotation, deckContainer);
        }
        else
        {
            GameObject emptyDeckPile = Instantiate(emptyPilePrefab, deckContainer.position, deckContainer.rotation, deckContainer);
            emptyDeckPile.GetComponentInChildren<TextMeshProUGUI>().text = "Deck";
        }
    }
    
    private void UpdateDiscardPileDisplay()
    {
        foreach (Transform child in discardPileContainer)
        {
            Destroy(child.gameObject);
        }

        if (discardPile.Count > 0)
        {
            Transform cardTransform = Instantiate(discardPile[discardPile.Count - 1].cardPrefab, discardPileContainer.position, discardPileContainer.rotation, discardPileContainer);
            Card card = cardTransform.GetComponent<Card>();
            card.InitializeCard(discardPile[discardPile.Count - 1]);
            cardTransform.localPosition = Vector3.zero;
            
            card.enabled = false;
        }
        else
        {
            GameObject emptyDiscardPile = Instantiate(defaultEmptyPilePrefab, discardPileContainer.position, discardPileContainer.rotation, discardPileContainer);
            emptyDiscardPile.GetComponentInChildren<TextMeshProUGUI>().text = "Discard";
        }
    }
}
