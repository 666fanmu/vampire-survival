using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private static PlayerHealth instance;
    public static PlayerHealth Instance
    {
        get 
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlayerHealth>() as PlayerHealth;
            }

            return instance;
        }
    }


    public float CurrentHealth, MaxHealth;

    public Slider HealthSlider; 

    private void Start()
    {
        CurrentHealth = MaxHealth;
        HealthSlider.value = MaxHealth;
    }

    private void Update()
    {
        
    }


    public void GetDamage(float damage)
    {
        CurrentHealth -= damage;
        HealthSlider.value = CurrentHealth;
        if (CurrentHealth<=0)
        {
            gameObject.SetActive(false);
        }
    }
}
