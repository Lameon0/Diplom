using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpPanel : MonoBehaviour
{
    [SerializeField] private Image healthBar; // ������ �� ����������� ������ ��������
    [SerializeField] private PlayerLogic playerLogic; // ������ �� ������ ������, ��� �������� ��������

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
            // ��������� ���� ����������� ��������
            float healthFraction = playerLogic.Hp / playerLogic.MaxHp;
            healthBar.fillAmount = healthFraction; // ��������� ���������� ������
        }
    }
}
