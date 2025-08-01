using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] public bool locked = false; // ���������� ��� ���������� �����
    [SerializeField] public float moveDistance = 2f; // ���������, �� ������� ������������ �����

    private Vector3 originalPosition; // �������� ������� �����
    [SerializeField] public bool isOpening = false; // ���� ��� ������������ ��������� ���������� �����

    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        if (locked == false && isOpening == true)
        {
            // ���� ����� ������������� � ��� ��� �� �����������, �������� ����������
            isOpening = true;
            Vector3 targetPosition = originalPosition + Vector3.up * moveDistance; // ������� ������� �����
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
        float moveTime = 1f; // ����� �������� �����

        Vector3 startPosition = transform.position;

        while (elapsedTime < moveTime)
        {
            // ������ ���������� ����� � ������� �������
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / moveTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ������������� ����� �� ������� �������
        transform.position = targetPosition;
    }
    IEnumerator MoveDoorClose(Vector3 targetPosition)
    {
        float elapsedTime = 0f;
        float moveTime = 1f; // ����� �������� �����

        Vector3 startPosition = transform.position;

        while (elapsedTime < moveTime)
        {
            // ������ ���������� ����� � ������� �������
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / moveTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ������������� ����� �� ������� �������
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
