using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Mine : MonoBehaviour
{
    [SerializeField] int damage = 10; // ����, ������� ������� ����
    [SerializeField] GameObject explosionPrefab; // ������ ������

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // �������� ��������� PlayerLogic � ������
            PlayerLogic player = other.GetComponent<PlayerLogic>();
            if (player != null)
            {
                // ��������� �������� ������ �� ���������� �����
                player.Hp -= damage;
            }

            // ������� �����
            if (explosionPrefab != null)
            {
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            }

            // ���������� ����
            Destroy(gameObject);
        }
    }
}
