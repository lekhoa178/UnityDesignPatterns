﻿using TMPro;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    TMP_Text healthText;

    private void Awake()
    {
        healthText = GetComponent<TMP_Text>();
    }

    void UpdateHealthDisplay(int health)
    {
        healthText.text = $"Health: {health}";
    }
}