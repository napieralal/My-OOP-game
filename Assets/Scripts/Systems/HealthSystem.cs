using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Health System", menuName = "ScriptableObjects/Systems/Health")]
public class HealthSystem : ScriptableObject
{
    [SerializeField] public int health { get; private set; }
    [SerializeField] private int maxHealth;
    [SerializeField] public int shield { get; private set; }
    [SerializeField] private int maxShield;
    [SerializeField] private float changeSpeed;

    [System.NonSerialized] public UnityEvent<int> healthChangeEvent;

    [SerializeField] private GameObject healthBarPrefab;

    private Image healthBar;
    private Image shieldBar;
    private TextMeshPro healthBarText;
    private Transform currentBorder;
    [SerializeField] private Gradient colorGradient;

    private void OnEnable()
    {
        health = maxHealth;
        shield = 0;
        if (healthChangeEvent == null)
        {
            healthChangeEvent = new UnityEvent<int>();
        }
        healthChangeEvent.AddListener(OnHealthChanged);
    }

    public void InitializeHealthBar(Transform parentTransform)
    {
        if (healthBarPrefab != null)
        {
            GameObject healthBarInstance = Instantiate(healthBarPrefab, parentTransform);
            Transform canvasTransform = healthBarInstance.transform.Find("Canvas");
            Transform barFillTransform = canvasTransform.Find("BarFill");
            healthBar = barFillTransform.GetComponent<Image>();
            Transform shieldBarTransform = canvasTransform.Find("Shield");
            shieldBar = shieldBarTransform.GetComponent<Image>();
            healthBarText = healthBarInstance.transform.Find("HealthText").GetComponent<TextMeshPro>();
            currentBorder = healthBarInstance.transform.Find("Current Border");
            UpdateHealthBar();
        }
    }

    private void OnHealthChanged(int currentHealth)
    {
        UpdateHealthBar();
    }

    public void DecreaseHealth(int damage, bool isMagic)
    {
        if (shield > 0)
        { 
            damage = isMagic ? damage * 2 : damage;
            int shieldDamage = Mathf.Min(shield, damage);
            shield -= shieldDamage;
            damage -= shieldDamage;
            damage = isMagic ? damage / 2 : damage;
            Debug.Log("Shield absorbed " + shieldDamage + " damage, remaining shield: " + shield);
        }
        
        if (damage > 0)
        {
            health -= damage;
            health = Mathf.Max(0, health);
            Debug.Log("Health decreased to " + health);
        }
        
        healthChangeEvent.Invoke(health);
    }

    public void IncreaseHealth(int recover)
    {
        health += recover;
        health = Mathf.Min(health, maxHealth);
        healthChangeEvent.Invoke(health);
    }
    
    public void IncreaseShield(int amount)
    {
        shield += amount;
        shield = Mathf.Min(shield, maxShield);
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null && healthBarText != null && shieldBar != null)
        {
            float healthPercentage = (float)health / maxHealth;
            float shieldPercentage = (float)shield / maxShield;
            healthBar.DOFillAmount(healthPercentage, changeSpeed);
            healthBar.DOColor(colorGradient.Evaluate(healthPercentage), changeSpeed);
            
            if (shield > 0)
            {
                shieldBar.gameObject.SetActive(true);
                shieldBar.DOFillAmount(shieldPercentage, changeSpeed);
                healthBarText.text = $"{shield}/{maxShield}";
            }
            else
            {
                shieldBar.gameObject.SetActive(false);
                healthBarText.text = $"{health}/{maxHealth}";
            }
        }
    }
    
    public void SetCurrentBorderActive(bool isActive)
    {
        if (currentBorder != null)
        {
            currentBorder.gameObject.SetActive(isActive);
        }
    }
}
