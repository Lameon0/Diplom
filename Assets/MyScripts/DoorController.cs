using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] public bool locked = false; // Переменная для блокировки двери
    [SerializeField] public float moveDistance = 2f; // Дистанция, на которую отодвигается дверь

    private Vector3 originalPosition; // Исходная позиция двери
    [SerializeField] public bool isOpening = false; // Флаг для отслеживания состояния открывания двери

    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        if (locked == false && isOpening == true)
        {
            // Если дверь заблокирована и она еще не открывается, начинаем открывание
            isOpening = true;
            Vector3 targetPosition = originalPosition + Vector3.up * moveDistance; // Целевая позиция двери
            StartCoroutine(MoveDoor(targetPosition));
        }
        if (isOpening == false)
        {
            Vector3 targetPosition = originalPosition;
            StartCoroutine(MoveDoorClose(originalPosition));
        }
    }

    IEnumerator MoveDoor(Vector3 targetPosition)
    {
        float elapsedTime = 0f;
        float moveTime = 1f; // Время движения двери

        Vector3 startPosition = transform.position;

        while (elapsedTime < moveTime)
        {
            // Плавно перемещаем дверь к целевой позиции
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / moveTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Устанавливаем дверь на целевую позицию
        transform.position = targetPosition;
    }
    IEnumerator MoveDoorClose(Vector3 targetPosition)
    {
        float elapsedTime = 0f;
        float moveTime = 1f; // Время движения двери

        Vector3 startPosition = transform.position;

        while (elapsedTime < moveTime)
        {
            // Плавно перемещаем дверь к целевой позиции
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / moveTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Устанавливаем дверь на целевую позицию
        transform.position = originalPosition;
    }
    public void ButtonPressed()
    {
        if(isOpening == false)
        {
            isOpening = true;
        }
        else
        {
            isOpening = false;
        }
    }
    public void LockerUsed()
    {
        if (locked == true)
        {
            locked = false;
        }
    }
}
