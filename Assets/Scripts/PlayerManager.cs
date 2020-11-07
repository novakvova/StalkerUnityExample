using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }

    [Header("Health")]
    private Text HealthText;
    public int health;
    public int maxHealth { get; private set; }

    [Header("Armour")]
    private Text ArmourText;
    public int armour;
    public int maxrArmour { get; private set; }

    public void Start()
    {
        HealthText = GameObject.Find("HealthCount").GetComponent<Text>();
        ArmourText = GameObject.Find("ArmourCount").GetComponent<Text>();

        maxHealth = 100;
        HealthText.text = health.ToString();

        maxrArmour = 100;
        ArmourText.text = armour.ToString();
    }

    public void Startup()
    {
        //Debug.Log("Player manager started...");

        //health = 50;
        //maxHealth = 100;
        //HealthText.text = health.ToString();
    
        //status = ManagerStatus.Started;
    }

    public void ChangeArmour(int value)
    {
        armour += value;
        Mathf.Clamp(armour, 0, maxrArmour);

        ArmourText.text = armour.ToString();
    }

    public void AddHealth(int value)
    {
       
        health += value;

        health = Mathf.Clamp(health, 0, maxHealth);

        HealthText.text = health.ToString();
        ArmourText.text = armour.ToString();
    }
    public void MinusHealth(int value)
    {
        if (armour > 0)
        {
            if (value > armour)
            {
                health -= value - armour;
                armour = 0;
            } else
            {
                armour -= value;
            }
        }
        else
        {
            health -= value;
        }

        health = Mathf.Clamp(health, 0, maxHealth);

        HealthText.text = health.ToString();
        ArmourText.text = armour.ToString();

        if (health <= 0)
            Die();
    }

    void Die()
    {
        Application.LoadLevel("Priject");
    }
}
