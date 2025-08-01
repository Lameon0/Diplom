using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animControll : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Enemyscript enemyScript;

    void Start()
    {
        // ѕровер€ем, что аниматор и скрипт врага назначены
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
        // ѕровер€ем, что скрипт врага и аниматор назначены
        if (enemyScript != null && animator != null)
        {
            // ”станавливаем значение параметра HP в аниматоре равным текущему здоровью врага
            animator.SetBool("isDead", enemyScript.isDead);
            animator.SetBool("IsAttack", enemyScript.IsAttack);
            animator.SetBool("isRunning", enemyScript.isRunning);
        }
    }
}
