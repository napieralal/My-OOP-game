using System;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

[CreateAssetMenu(fileName = "Health System", menuName = "ScriptableObjects/Systems/Health")]
public class HealthSystem : ScriptableObject
{
    [SerializeField] public int health { get; private set; } = 100;
    [SerializeField] public int maxHealth { get; private set; } = 100;

    [System.NonSerialized] public UnityEvent<int> healthChangeEvent;

    [SerializeField] private GameObject healthBarPrefab;

    private Transform barTransform;
    private TextMeshPro healthBarText;

    private void OnEnable()
    {
        health = maxHealth;
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
            barTransform = healthBarInstance.transform.Find("Bar");
            healthBarText = healthBarInstance.transform.Find("HealthText").GetComponent<TextMeshPro>();
        }
    }

    private void OnHealthChanged(int currentHealth)
    {
        UpdateHealthBar();
    }

    public void DecreaseHealth(int damage)
    {
        health -= damage;
        health = Mathf.Max(0, health);
        Debug.Log("Health decreased to " + health);
        healthChangeEvent.Invoke(health);
    }

    public void IncreaseHealth(int recover)
    {
        health += recover;
        health = Mathf.Min(health, maxHealth);
        healthChangeEvent.Invoke(health);
    }

    private void UpdateHealthBar()
    {
        if (barTransform != null && healthBarText != null)
        {
            float healthPercentage = (float)health / maxHealth;
            barTransform.localScale = new Vector3(healthPercentage, 1, 1);

            // Update the health text
            healthBarText.text = $"{health}/{maxHealth}";
        }
    }
}
