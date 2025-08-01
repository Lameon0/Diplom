using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    [SerializeField] float lifespan = 0.3f; // ����� ����� �������
    [SerializeField] float damage = 4f; // ����, ������� ������� ������
    [SerializeField] float speed = 1f;
    // Start is called before the first frame update
    void Start()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // �������� ��������� Enemyscript � �����
            PlayerLogic Player = other.GetComponent<PlayerLogic>();
            if (Player != null)
            {
                // ��������� �������� ������ �� ���������� �����
                Player.Hp -= damage;
                // ���������� ������ ����� ���������
                Destroy(gameObject);
                // ���������, �������� �� �������� ����� ����
                if (Player.Hp <= 0)
                {
                    // ���� ��, �������� ����� ������ ������
                    Player.GameOver();
                }
            }

        }
    }
    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime); // ����������� �������� ����� �����
        lifespan -= 1 * Time.deltaTime;
        if (lifespan <= 0)  // ����������� ����� �� ��������� ������� �����
        {
            Destroy(gameObject);
        }
    }
}
