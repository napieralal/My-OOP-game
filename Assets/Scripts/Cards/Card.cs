using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private CardSO cardData;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Image cardImage;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private TextMeshProUGUI descText;
    [SerializeField] private TextMeshProUGUI actionText;
    [SerializeField] private TextMeshProUGUI specialText;
    private bool hasSpecial;

    [SerializeField] private Image costBorder;
    [SerializeField] private Image specialBorder;
    
    [SerializeField] private float moveDuration = 0.5f;
    [SerializeField] private float hoverDelay = 0.05f;
    private Vector3 originalPosition;
    private Vector3 hoverPosition;
    private Coroutine hoverCoroutine;

    private void Start()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.anchoredPosition;
        hoverPosition = new Vector2(originalPosition.x, 100f);
    }
    
    public void InitializeCard(CardSO cardSO)
    {
        cardData = cardSO;
        nameText.text = cardData.cardName;
        if (cardData.cardImage != null)
        {
            cardImage.sprite = cardData.cardImage;
        }
        CheckCost();
        if (cardData.cardDesc != null)
        {
            descText.text = cardData.cardDesc;
        }
        actionText.text = cardData.cardAction;
        CheckSpecial();
    }
    
    private Sprite TextureToSprite(Texture2D texture)
    {
        Rect rect = new Rect(0, 0, texture.width, texture.height);
        return Sprite.Create(texture, rect, new Vector2(0.5f, 0.5f), 100.0f);
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverCoroutine == null)
        {
            hoverCoroutine = StartCoroutine(HoverCard());
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (hoverCoroutine != null)
        {
            StopCoroutine(hoverCoroutine);
            hoverCoroutine = null;
        }
        StartCoroutine(MoveCardToOriginalPosition());
    }
    
    private IEnumerator HoverCard()
    {
        yield return new WaitForSeconds(hoverDelay);

        RectTransform rectTransform = GetComponent<RectTransform>();
        Vector2 startPosition = rectTransform.anchoredPosition;
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            rectTransform.anchoredPosition = Vector2.Lerp(startPosition, hoverPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        rectTransform.anchoredPosition = hoverPosition;
        hoverCoroutine = null;
    }

    private IEnumerator MoveCardToOriginalPosition()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        Vector2 startPosition = rectTransform.anchoredPosition;
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            rectTransform.anchoredPosition = Vector2.Lerp(startPosition, originalPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        rectTransform.anchoredPosition = originalPosition;
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        CardAction();
        CardManager cardManager = FindObjectOfType<CardManager>();
        cardManager.DiscardCard(cardData);
        Destroy(gameObject);
    }

    private void CardAction()
    {
        Debug.Log("Card played!");
        foreach (CardEffectSO effect in cardData.cardEffects)
        {
            effect.ApplyEffect();
        }
    }

    private void CheckSpecial()
    {
        if (cardData.cardSpecial < 1)
        {
            hasSpecial = false;
        }
        else
        {
            hasSpecial = true;
            specialBorder.gameObject.SetActive(true);
            specialText.text = cardData.cardSpecial.ToString();
        }
    }

    private void CheckCost()
    {
        if (cardData.cardCost >= 1)
        {
            costBorder.gameObject.SetActive(true);
            costText.text = cardData.cardCost.ToString();
        }
    }
    
    
}