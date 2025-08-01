using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animControll : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Enemyscript enemyScript;

    void Start()
    {
        // ���������, ��� �������� � ������ ����� ���������
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        if (enemyScript == null)
        {
            enemyScript = GetComponent<Enemyscript>();
        }
    }

    void Update()
    {
        // ���������, ��� ������ ����� � �������� ���������
        if (enemyScript != null && animator != null)
        {
            // ������������� �������� ��������� HP � ��������� ������ �������� �������� �����
            animator.SetBool("isDead", enemyScript.isDead);
            animator.SetBool("IsAttack", enemyScript.IsAttack);
            animator.SetBool("isRunning", enemyScript.isRunning);
        }
    }
}
