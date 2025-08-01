using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardLocker : MonoBehaviour
{
    [SerializeField] private GameObject door; // ������ �� ������� ������ �����

    private void Start()
    {
        ChangeColor(Color.red);
    }

    private void OnTriggerEnter(Collider other)
    {
        // ���������, ��������� �� ������� ������ ����� � �������
        if (other.CompareTag("Key"))
        {
            // ������� ���������� �����
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
