using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
    private int health;
    public GameObject AmmoBoxPrefab;

    [SerializeField] private Slider healthBar;

    [SerializeField] private Image healthBarFillImage;
    [SerializeField] private Color maxColor;
    [SerializeField] private Color minColor;

    private int currentHealth;

    private void Start()
    {
        currentHealth = 100;
        health = 100;

        SetHealthBar();
    }

    public void Damage(int value)
    {
        currentHealth -= value;
        SetHealthBar();

        if (currentHealth <= 0)
            Die();
    }

    private void Die()
    {
        Transform pos = transform;

        Destroy(this.gameObject);
        if (Random.Range(0, 100) > 85)
        {
            Instantiate(AmmoBoxPrefab,pos.position,pos.rotation);
        }
    }

    private void SetHealthBar()
    {
        float healthPercentage = CalculateHealth();
        healthBar.value = healthPercentage;
        healthBarFillImage.color = Color.Lerp(minColor, maxColor, healthPercentage);
    }

    private float CalculateHealth()
    {
        return ((float)currentHealth / (float)100);
    }
}
