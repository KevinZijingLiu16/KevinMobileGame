using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{

    [SerializeField] Image AmountImage;
    [SerializeField] TextMeshProUGUI AmountText;

    internal void UpdateHealth(float health, float delta, float maxHealth)
    {
        AmountImage.fillAmount = health / maxHealth;
        AmountText.SetText(health.ToString());
    }
}
