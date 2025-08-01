using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Mine : MonoBehaviour
{
    [SerializeField] int damage = 10; // Урон, который наносит мина
    [SerializeField] GameObject explosionPrefab; // Префаб взрыва

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Получаем компонент PlayerLogic у игрока
            PlayerLogic player = other.GetComponent<PlayerLogic>();
            if (player != null)
            {
                // Уменьшаем здоровье игрока на количество урона
                player.Hp -= damage;
            }

            // Создаем взрыв
            if (explosionPrefab != null)
            {
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            }

            // Уничтожаем мину
            Destroy(gameObject);
        }
    }
}
