using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tryteleport : MonoBehaviour
{
    [SerializeField] private Transform teleportTarget; // Целевая позиция для телепортации

    // Метод для телепортации игрока
    public void TeleportPlayerToTarget()
    {
        // Находим объект игрока
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Debug.Log("Ищем игрока...");

        if (player != null)
        {
            Debug.Log("Игрок найден.");

            // Проверяем, есть ли целевая позиция для телепортации
            if (teleportTarget != null)
            {
                Debug.Log("Целевая точка найдена. Телепортируем игрока.");

                // Телепортируем игрока к указанной позиции
                player.transform.position = teleportTarget.position;
                player.transform.rotation = teleportTarget.rotation;

                Debug.Log($"Игрок телепортирован в позицию: {teleportTarget.position}");
            }
            else
            {
                Debug.LogWarning("Teleport target is not set!");
            }
        }
        else
        {
            Debug.LogWarning("Player object not found!");
        }
    }
}
