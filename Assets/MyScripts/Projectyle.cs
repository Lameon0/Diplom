using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectyle : MonoBehaviour
{
    [SerializeField] float speed = 10f; // �������� �������
    [SerializeField] float lifespan = 3f; // ����� ����� �������
    [SerializeField] int damage = 1; // ����, ������� ������� ������

    private float spawnTime; // ����� �������� �������

    private void Start()
    {
        // ���������� ����� �������� �������
        spawnTime = Time.time;
    }

    private void Update()
    {
        // ���������� ������ ������
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // ���������, ������� �� ����� ����� �������
        if (Time.time - spawnTime >= lifespan)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // ���������, ���������� �� ������ � �������� � ����� "Enemy"
        if (other.CompareTag("Enemy"))
        {
            // �������� ��������� Enemyscript � �����
            Enemyscript enemy = other.GetComponent<Enemyscript>();
            if (enemy != null)
            {
                // ��������� �������� ����� �� ���������� �����
                enemy.HP -= damage;

                // ���������, �������� �� �������� ����� ����
                if (enemy.HP <= 0)
                {
                    // ���� ��, �������� ����� ������ �����
                    enemy.Death();
                }
            }

            // ���������� ������ ����� ���������
            Destroy(gameObject);
        }
    }
}
