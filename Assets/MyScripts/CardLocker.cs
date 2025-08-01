using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardLocker : MonoBehaviour
{
    [SerializeField] private GameObject door; // Ссылка на игровой объект двери

    private void Start()
    {
        ChangeColor(Color.red);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, находится ли игровой объект рядом с панелью
        if (other.CompareTag("Key"))
        {
            // Снимаем блокировку двери
            if (door != null)
            {
                DoorController doorController = door.GetComponent<DoorController>();
                if (doorController != null)
                {
                    doorController.LockerUsed();
                    ChangeColor(Color.green);
                }
            }
        }

    }
    private void ChangeColor(Color newColor)
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = newColor;
        }
    }
}
