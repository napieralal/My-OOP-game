using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public CardSO card;
    
    [SerializeField] private TextMeshProUGUI nameText;
    public Sprite cardImage;
    public TextMeshProUGUI costText;
    public TextMeshProUGUI descText;
    public TextMeshProUGUI actionText;
    public TextMeshProUGUI specialText;
    public bool hasSpecial;

    public Image specialBorder;

    void Start()
    {
        if (card.cardSpecial < 1)
        {
            hasSpecial = false;
        }
        else
        {
            hasSpecial = true;
            specialBorder.gameObject.SetActive(true);
            specialText.text = card.cardSpecial.ToString();
        }

        nameText.text = card.cardName;
        cardImage = card.cardImage;
        costText.text = card.cardCost.ToString();
        descText.text = card.cardDesc;
        actionText.text = card.cardAction;
    }
}
