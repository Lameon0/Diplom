using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpPanel : MonoBehaviour
{
    [SerializeField] private Image healthBar; // Ссылка на изображение панели здоровья
    [SerializeField] private PlayerLogic playerLogic; // Ссылка на скрипт игрока, где хранится здоровье

    private void Start()
    {
        if (playerLogic == null)
        {
            Debug.LogError("PlayerLogic reference is not set");
        }
    }

    private void Update()
    {
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null && playerLogic != null)
        {
            // Вычисляем долю оставшегося здоровья
            float healthFraction = playerLogic.Hp / playerLogic.MaxHp;
            healthBar.fillAmount = healthFraction; // Обновляем заполнение панели
        }
    }
}
