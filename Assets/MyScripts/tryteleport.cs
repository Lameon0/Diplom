using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tryteleport : MonoBehaviour
{
    [SerializeField] private Transform teleportTarget; // ������� ������� ��� ������������

    // ����� ��� ������������ ������
    public void TeleportPlayerToTarget()
    {
        // ������� ������ ������
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Debug.Log("���� ������...");

        if (player != null)
        {
            Debug.Log("����� ������.");

            // ���������, ���� �� ������� ������� ��� ������������
            if (teleportTarget != null)
            {
                Debug.Log("������� ����� �������. ������������� ������.");

                // ������������� ������ � ��������� �������
                player.transform.position = teleportTarget.position;
                player.transform.rotation = teleportTarget.rotation;

                Debug.Log($"����� �������������� � �������: {teleportTarget.position}");
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
